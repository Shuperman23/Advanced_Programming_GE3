using ControlBiblioteca.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ControlBiblioteca.Data;
using Microsoft.OpenApi.Models;
using ControlBiblioteca.Middleware;
using ControlBiblioteca.Middleware.Middleware;

namespace ControlBiblioteca
{
    public class Startup
    {
        // Constructor de la clase Startup que recibe IConfiguration como parámetro
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Propiedad de solo lectura para acceder a la configuración de la aplicación
        public IConfiguration Configuration { get; }

        // Método para configurar los servicios de la aplicación
        public void ConfigureServices(IServiceCollection services)
        {
            // Agrega la política CORS para permitir peticiones desde cualquier origen
            services.AddCors();

            // Configura las opciones de serialización JSON para evitar referencias circulares
            services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

            // Agrega la generación de documentación Swagger
            services.AddEndpointsApiExplorer();

            // Configura el contexto de la base de datos utilizando SQL Server
            services.AddDbContext<BIBLIOTECAContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

            // Configura la generación de la documentación Swagger
            services.AddSwaggerGen(c =>
            {
                // Agrega la información básica del API
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "1.0.0.4" });

                // Configura el esquema de seguridad Bearer
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
            });

            // Configura AutoMapper
            services.AddAutoMapper(typeof(Startup));
        }

        // Método para configurar la aplicación y el entorno de ejecución
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //acá se agrega la "dirección" al middleware
            app.UseMiddleware<ExceptionMiddleware>();
            // Habilita el uso de Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI"); });

            // Redirecciona las solicitudes HTTP a HTTPS
            app.UseHttpsRedirection();
            app.UseRouting();

            // Habilita la política CORS configurada anteriormente
            app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());

            // Habilita la redirección HTTPS
            app.UseHttpsRedirection();

            // Habilita la autenticación y autorización
            app.UseAuthentication();
            app.UseAuthorization();

            // Configura los endpoints de la aplicación
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}