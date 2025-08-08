namespace GroovE.Application.UseCases.Auth;

public interface IAuthenticationService
{
    Task<string> LoginUser(string email, string password, bool rememberMe);
    Task<string> RegisterUser(string email, string password, string firstName, string lastName);
    Task ConfirmEmailAsync(string userId, string code);
}
