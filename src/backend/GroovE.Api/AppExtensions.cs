namespace GroovE.Api;

public static class AppExtensions
{
    public static void UsePresentation(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();

        app.MapEndpoints();
    }
}
