using System.Net;
using System.Text.Json;
using GulbahceSepeti.Application.Common.Exceptions;

namespace GulbahceSepeti.API.Middleware;

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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (status, message, errors) = exception switch
        {
            NotFoundException e      => (HttpStatusCode.NotFound, e.Message, (object?)null),
            ForbiddenException e     => (HttpStatusCode.Forbidden, e.Message, null),
            BusinessRuleException e  => (HttpStatusCode.BadRequest, e.Message, null),
            ValidationException e    => (HttpStatusCode.UnprocessableEntity, e.Message, (object)e.Errors),
            _                        => (HttpStatusCode.InternalServerError, "Beklenmeyen bir hata oluştu.", null)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        var body = JsonSerializer.Serialize(new
        {
            statusCode = (int)status,
            message,
            errors
        }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        return context.Response.WriteAsync(body);
    }
}
