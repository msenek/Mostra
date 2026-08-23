using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mostra.Application.Common.Behaviors;
using Mostra.Application.Interfaces;
using Mostra.Application.Products.CreateProduct;
using Mostra.Infraestructure.Repository;
using Mostra.Infrastructure.Persistence;
using NSwag;
using NSwag.Generation.Processors.Security;

var builder = WebApplication.CreateBuilder(args);

// --- Services ---
builder.Services.AddControllers();

// NSwag: genera el openapi.json + sirve la UI
builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "Mostra API";
    config.Version = "v1";
    config.Description = "API para que comerciantes publiquen sus catálogos con precios vía QR.";
});

// Repositorios (registro manual, porque NO son handlers de MediatR)
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// DbContext
builder.Services.AddDbContext<MostraContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

// MediatR: escanea el assembly de Application y registra TODOS los handlers automáticamente
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<CreateProductRequestDto>());

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

var app = builder.Build();

// --- Pipeline ---
if (app.Environment.IsDevelopment())
{
    // Sirve el openapi.json en /swagger/v1/swagger.json
    app.UseOpenApi();

    // Sirve la UI en /swagger
    app.UseSwaggerUi(settings =>
    {
        settings.Path = "/swagger";
        settings.DocumentPath = "/swagger/v1/swagger.json";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();