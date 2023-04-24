using API.Extensions;
using API.Helpers.Errors;
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

//Manejo de errores del model state(Anotaciones como Required, Email, etc)
builder.Services.AddValidationErrors();

builder.Services.AddDbContext<BusinessContext>(options =>
{
    var connectionString = Environment.GetEnvironmentVariable("TEMPALERT_BUSINESS_DATABASE_CONNECTION");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Middleware para el manejo de excepciones globales
app.UseMiddleware<ExceptionMiddleware>();

//Manejo de errror de metodo no encontrado
app.UseStatusCodePagesWithReExecute("/errors/{0}");

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

//app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
