namespace GroovE.Application.UseCases.Auth;

public interface IAuthenticationService
{
    Task<string> LoginUser(string email, string password, bool rememberMe);
}
