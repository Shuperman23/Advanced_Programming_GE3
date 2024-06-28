using ExamenCGastos.Models;
using System.Net;
using System.Text.Json;
using ExamenCGastos.DTOs;
using ExamenCGastos.Interfaces;


namespace ExamenCGastos.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Invoca el siguiente middleware en la cadena
            }
            catch (Exception ex)
            {
                // Registra el error en los logs
                _logger.LogError(ex, message: "An exception has occurred: {ExceptionMessage}", ex.Message);

                // Guarda el error en la base de datos a través del unitOfWork
                await Utilities.CreateNewErrorAsync(new ErrorLogDto
                {
                    Controller = context.Request.Path,
                    Endpoint = context.Request.Method,
                    ErrorMessage = ex.Message,
                    ErrorStackTrace = ex.StackTrace
                });

                // Configura la respuesta de error para el cliente
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new ApiRequestResultDto<string>
                {
                    Success = false,
                    Message = "Error while processing operation"
                };

                // Agrega detalles adicionales al mensaje de error si estamos en entorno de desarrollo
                if (_env.IsDevelopment())
                {
                    response.Message += $": {ex.Message}";
                    response.Result = $"Error: {ex.StackTrace?.ToString()}";
                }
                else
                {
                    response.Message += $" Internal Server Error.";
                }

                // Serializa la respuesta en formato JSON
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);

                // Envía la respuesta JSON al cliente
                await context.Response.WriteAsync(json);
            }
        }
    }
}
