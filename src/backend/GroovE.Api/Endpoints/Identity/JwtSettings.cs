namespace GroovE.Api.Endpoints.Identity;

public class JwtSettings
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public int ExpirationInHoursDefault { get; set; } = 3;
    public int ExpirationInHoursRememberMe { get; set; } = 30 * 24;
}
