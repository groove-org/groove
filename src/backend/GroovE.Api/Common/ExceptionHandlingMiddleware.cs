using Ardalis.GuardClauses;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace GroovE.Api.Common;

public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");
            await WriteErrorResponseAsync(context, ex);
        }
    }

    private static Task WriteErrorResponseAsync(HttpContext ctx, Exception ex)
    {
        ProblemDetails problem;
        int status;

        switch (ex)
        {
            case ValidationException validationException:
                status = StatusCodes.Status422UnprocessableEntity;
                var errors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(x => x.ErrorMessage).ToArray()
                    );

                problem = new ValidationProblemDetails(errors)
                {
                    Title = "Validation failed",
                    Status = status
                };
                break;

            case NotFoundException notFoundException:
                status = StatusCodes.Status404NotFound;
                problem = new ProblemDetails
                {
                    Title = "Not Found",
                    Detail = notFoundException.Message,
                    Status = status,
                    Type = "https://httpstatuses.com/404"
                };
                break;

            case UnauthorizedAccessException unauthorizedAccessException:
                status = StatusCodes.Status401Unauthorized;
                problem = new ProblemDetails
                {
                    Title = "Unauthorized",
                    Detail = unauthorizedAccessException.Message,
                    Status = status,
                    Type = "https://httpstatuses.com/401"
                };
                break;

            default:
                status = StatusCodes.Status500InternalServerError;
                problem = new ProblemDetails
                {
                    Title = "An unexpected error occurred",
                    Detail = ex.Message,
                    Status = status,
                    Type = "https://httpstatuses.com/500"
                };
                break;
        }

        ctx.Response.ContentType = "application/problem+json";
        ctx.Response.StatusCode = status;
        return ctx.Response.WriteAsJsonAsync(problem);
    }
}