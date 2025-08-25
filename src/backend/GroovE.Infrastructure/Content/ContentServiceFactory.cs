using GroovE.Application.Content;
using GroovE.Infrastructure.Content.FileService;
using GroovE.Infrastructure.Content.S3Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minio;

namespace GroovE.Infrastructure.Content;

internal static class ContentServiceFactory
{
    public static IContentService Create(IServiceProvider provider)
    {
        var config = provider.GetRequiredService<IOptions<ContentConfiguration>>().Value;
        return config.Service switch
        {
            ContentServiceType.File => ActivatorUtilities.CreateInstance<FileContentService>(provider),
            ContentServiceType.S3 => ActivatorUtilities.CreateInstance<S3ContentService>(provider, new MinioClientFactory(options => options
                .WithCredentials(config.S3Configuration.AccessKey, config.S3Configuration.SecretKey)
                .WithEndpoint(config.S3Configuration.Host)
                .WithSSL(config.S3Configuration.UseSSL)).CreateClient()),
            _ => throw new ArgumentOutOfRangeException(nameof(config.Service), config.Service, "Unknown content service type")
        };
    }
}
