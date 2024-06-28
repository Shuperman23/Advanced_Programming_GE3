namespace ControlBiblioteca.Middleware
{
    using System;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using ControlBiblioteca.Models;
    using System.Security.Policy;
    using ControlBiblioteca.Data;
    using Microsoft.EntityFrameworkCore;

    namespace Middleware
    {
        public class ExceptionMiddleware
        {
            private readonly RequestDelegate next;
            private readonly ILogger<ExceptionMiddleware> logger;
            private readonly IHostEnvironment env;
            //el Service Provider con lo que investigué sirve para poder referenciar el DBcontext al Middleware
            private readonly IServiceProvider serviceProvider;

            public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env, IServiceProvider serviceProvider)
            {
                this.next = next;
                this.logger = logger;
                this.env = env;
                this.serviceProvider = serviceProvider;
            }

            public async Task InvokeAsync(HttpContext context)
            {
                try
                {
                    await next(context);
                }
                catch (Exception ex)
                {
                    using (var scope = serviceProvider.CreateScope())
                    {
                        //Vean maes con este ServiceProvider el puede instanciar la db al middle sin que se caiga el programa
                        var dbContext = scope.ServiceProvider.GetRequiredService<BIBLIOTECAContext>();

                        //el logger es un bloq de notas 
                        logger.LogError(ex, ex.Message);
                        context.Response.ContentType = "application/json";

                        //quise manejar el codigo del error pero no se pedía en la investigación
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var EndPoints = $"{context.Request.Path}{context.Request.QueryString}";

                        // Crear una instancia de ErrorLog con los detalles del error
                        var response = env.IsDevelopment() ? new ErrorLog(0, context.Request.RouteValues["controller"]?.ToString(), EndPoints, ex.Message, ex.StackTrace?.ToString()) :
                        new ErrorLog(context.Response.StatusCode, "Internal Server Error");

                        //se guarda la info en la db
                        dbContext.ErrorLogs.Add(response);
                        await dbContext.SaveChangesAsync();

                        // Serializar la instancia de ErrorLog a JSON
                        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                        var json = JsonSerializer.Serialize(response, options);

                        await context.Response.WriteAsync(json);
                    }
                }
            }
        }
    }
}
