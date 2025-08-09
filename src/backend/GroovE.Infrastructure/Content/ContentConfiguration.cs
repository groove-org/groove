using GroovE.Infrastructure.Content.S3Service;

namespace GroovE.Infrastructure.Content;

internal class ContentConfiguration
{
    public ContentServiceType ContentService { get; set; }

    public S3Configuration S3Configuration { get; set; } = new();
}

internal enum ContentServiceType
{
    File,
    S3
}
