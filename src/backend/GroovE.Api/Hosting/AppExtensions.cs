using GroovE.Api.Common;
using GroovE.Infrastructure.Identity;

namespace GroovE.Api.Hosting;

public static class AppExtensions
{
    public static void UsePresentation(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapGroup("/api/identity").WithTags("Identity").MapIdentityApi<User>();
        app.MapGroup("/api").MapEndpoints();
    }
}
