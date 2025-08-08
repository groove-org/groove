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
        app.UseCors("AllowFrontend");

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();

        var identityGroup = app.MapGroup("/api/identity").WithTags("Identity");
        identityGroup.MapIdentityApi<User>();
        identityGroup.RemoveReplacedEndpoints();

        app.MapGroup("/api").MapEndpoints();
    }

    private static void RemoveReplacedEndpoints(this IEndpointRouteBuilder group)
    {
        string[] endpointsToRemove = ["/login", "/register", "/confirmEmail"];
        var dataSource = group.DataSources.FirstOrDefault();
        var newDataSource = new DefaultEndpointDataSource(dataSource.Endpoints.Where(ep => ep is RouteEndpoint re && !endpointsToRemove.Any(r => re.RoutePattern.RawText.Contains(r))));
        group.DataSources.Remove(dataSource);
        group.DataSources.Add(newDataSource);
    }
}
