namespace GroovE.Application.UseCases.Identity.Dtos;

public record LoginResponseDto(string? Token, bool RequiresTwoFactor);
