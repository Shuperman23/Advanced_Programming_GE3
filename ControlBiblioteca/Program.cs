// Importaci�n del espacio de nombres necesario para el c�digo
using ControlBiblioteca;
using ControlBiblioteca.Interfaces;
using ControlBiblioteca.Repositories;
using Microsoft.AspNetCore.Hosting;

// Creaci�n de un nuevo constructor para la aplicaci�n web utilizando la clase WebApplication
var builder = WebApplication.CreateBuilder(args);

// Agregar servicio del contenedor UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ApiKeyAttribute>();

// Creaci�n de una nueva instancia de la clase Startup, que se encarga de configurar la aplicaci�n
var startup = new Startup(builder.Configuration);

// Llamada al m�todo ConfigureServices de la clase Startup para configurar los servicios de la aplicaci�n
startup.ConfigureServices(builder.Services);

// Construcci�n de la aplicaci�n
var app = builder.Build();

// Llamada al m�todo Configure de la clase Startup para configurar la aplicaci�n y el entorno de ejecuci�n
startup.Configure(app, app.Environment);

// Ejecuci�n de la aplicaci�n
app.Run();