using System.Net;
using System.Text.Json;
using Domain.Exceptions;

namespace Api.Middlewares;

public class GlobalExceptionHandle(RequestDelegate next, ILogger<GlobalExceptionHandle> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = exception switch
        {
            NotFoundException notFoundEx => new
            {
                statusCode = (int)HttpStatusCode.NotFound,
                message = notFoundEx.Message
            },
            BadRequestException badRequestEx => new
            {
                statusCode = (int)HttpStatusCode.BadRequest,
                message = badRequestEx.Message
            },
            DomainException domainEx => new
            {
                statusCode = (int)HttpStatusCode.UnprocessableEntity,
                message = domainEx.Message
            },
            _ => new
            {
                statusCode = (int)HttpStatusCode.InternalServerError,
                message = "An unexpected error occurred."
            }
        };

        if (response.statusCode == (int)HttpStatusCode.InternalServerError)
        {
            logger.LogError(exception, "Unhandled exception occurred: {Message}", exception.Message);
        }

        context.Response.StatusCode = response.statusCode;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
    }
}
