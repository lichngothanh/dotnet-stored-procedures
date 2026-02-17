using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

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
        ProblemDetails problemDetails;

        switch (exception)
        {
            case NotFoundException ex:
                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Resource not found",
                    Detail = ex.Message
                };
                break;

            case BadRequestException ex:
                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad request",
                    Detail = ex.Message
                };
                break;

            case DomainException ex:
                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status422UnprocessableEntity,
                    Title = "Business rule violated",
                    Detail = ex.Message
                };
                break;

            default:
                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal server error",
                    Detail = "An unexpected error occurred."
                };

                logger.LogError(exception, "Unhandled exception occurred");
                break;
        }

        problemDetails.Instance = context.Request.Path;
        problemDetails.Extensions["traceId"] = context.TraceIdentifier;

        context.Response.StatusCode = problemDetails.Status.Value;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problemDetails);
    }

}
