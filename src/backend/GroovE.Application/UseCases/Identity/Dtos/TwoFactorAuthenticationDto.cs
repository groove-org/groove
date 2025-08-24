namespace GroovE.Application.UseCases.Identity.Dtos;

public record TwoFactorAuthenticationDto(string SharedKey, string AuthenticatorUri);
