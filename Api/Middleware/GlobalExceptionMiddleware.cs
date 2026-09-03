using System.Net;
using System.Text.Json;
using FluentValidation;
using Mostra.Application.Exceptions;

namespace Mostra.Api.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;  
        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Error de validación en {Path}", context.Request.Path); 

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
                var response = new { message = "Error de validación", details = errors };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (DomainException ex)
            {
                _logger.LogWarning(ex, "Excepción de dominio en {Path}: {Message}", context.Request.Path, ex.Message);
                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = "application/json";

                var response = new { message = ex.Message };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno no controlado en {Path}", context.Request.Path);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new { message = "Ocurrió un error interno inesperado en el servidor." };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}