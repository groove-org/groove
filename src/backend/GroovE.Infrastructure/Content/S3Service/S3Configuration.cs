namespace GroovE.Infrastructure.Content.S3Service;

public class S3Configuration
{
    public string Host { get; set; } = string.Empty;
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public bool UseSSL { get; set; } = true;
}
