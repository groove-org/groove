using GroovE.Api.Common;
using GroovE.Infrastructure.Identity;

namespace GroovE.Api;

public static class AppExtensions
{
    public static void UsePresentation(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapGroup("/api/auth").WithTags("Auth").MapIdentityApi<User>();

        app.MapEndpoints(app.Services);
        //app.MapControllers();
    }
}
