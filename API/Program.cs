using API.Extensions;
using AspNetCoreRateLimit;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Configuracion de AutoMapper para el mapeo de entidades a dtos y viceversa
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
//Configuracion para la limitacion de peticiones por un rango de tiempo
builder.Services.ConfigureRateLimitiong();

builder.Services.ConfigureCors();
builder.Services.AddAplicacionServices();
//Configuracion para el versionado de la API
builder.Services.ConfigureApiVersioning();

builder.Services.AddControllers();

builder.Services.AddDbContext<BusinessContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//Configuracion para la limitacion de peticiones por un rango de tiempo
app.UseIpRateLimiting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Habilitar migraciones en espera o faltantes, al ejecutar el programa
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<BusinessContext>();
        await context.Database.MigrateAsync();
        //await SecurityContextSeed.SeedRolsAsync(context, loggerFactory);
    }
    catch (Exception ex)
    {
        var _logger = loggerFactory.CreateLogger<Program>();
        _logger.LogError(ex, "Error ocurred from migration progress");
    }
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
