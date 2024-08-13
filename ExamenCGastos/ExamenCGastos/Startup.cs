using ExamenCGastos.Data;
using ExamenCGastos.Middleware;
using ExamenCGastos.Middleware.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace ExamenCGastos
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
                services.AddDbContext<CGASTOSContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CadenaSQL")));

            // Servicios Singleton
            services.AddSingleton<Utilities>();
            services.AddTransient<ConfiguracionEmail>();
            //   services.AddSingleton<EmailService>();


            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //        .AddJwtBearer(options =>
            //        {
            //            options.RequireHttpsMetadata = false;
            //            options.SaveToken = true;
            //            options.TokenValidationParameters = new TokenValidationParameters
            //            {
            //                ValidateIssuerSigningKey = true,
            //                ValidateIssuer = false,
            //                ValidateAudience = false,
            //                ValidateLifetime = true,
            //                ClockSkew = TimeSpan.Zero,
            //                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:key"]))
            //            };
            //        });
            // Configura la generación de la documentación Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "1.0.0.4" });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition("Bearer", securityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        securityScheme,
                        Array.Empty<string>()
                    }
                });
            });

            // Configura AutoMapper
            services.AddAutoMapper(typeof(Startup));
            var jwtSettings = Configuration.GetSection("Jwt");
            var key = jwtSettings["Key"];

            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("JWT Key is not configured in the appsettings.json file.");
            }

            var keyBytes = Encoding.ASCII.GetBytes(key);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ClockSkew = TimeSpan.Zero
                };
            });
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