using Microsoft.AspNetCore.Mvc;
namespace Api.Extensions;

public static class ModelStateConfig
{
    public static IServiceCollection AddModelStateConfig(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .ToDictionary(
                        x => x.Key,
                        x => x.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                var problemDetails = new ValidationProblemDetails(errors)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation failed",
                    Detail = "One or more validation errors occurred.",
                    Instance = context.HttpContext.Request.Path
                };

                return new BadRequestObjectResult(problemDetails);
            };
        });

        return services;
    }
}