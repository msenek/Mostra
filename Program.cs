using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mostra.Application.Common.Behaviors;
using Mostra.Application.Interfaces;
using Mostra.Application.Products.CreateProduct;
using Mostra.Infrastructure.Repository;
using Mostra.Infrastructure.Persistence;
using Mostra.Infrastructure.Repository;
using NSwag;
using NSwag.Generation.Processors.Security;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "Mostra API";
    config.Version = "v1";
    config.Description = "API para que comerciantes publiquen sus catálogos con precios vía QR.";
});


builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBusinessRepository, BusinessRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddDbContext<MostraContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<CreateProductRequestDto>());

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();

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