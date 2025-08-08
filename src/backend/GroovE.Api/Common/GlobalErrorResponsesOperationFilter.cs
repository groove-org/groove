using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GroovE.Api.Common;

public class GlobalErrorResponsesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var errorResponses = new[]
        {
            (422, "Validation failed"),
            (401, "Unauthorized"),
            (404, "Not Found"),
            (500, "Internal Server Error")
        };

        foreach (var (code, description) in errorResponses)
        {
            operation.Responses[code.ToString()] = new OpenApiResponse
            {
                Description = description
            };
        }
    }
}