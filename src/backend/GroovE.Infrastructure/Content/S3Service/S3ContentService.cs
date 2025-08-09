using GroovE.Application.Content;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace GroovE.Infrastructure.Content.S3Service;

internal class S3ContentService(IMinioClient client) : IContentService
{
    public async Task<Stream> GetFileContentAsync(string path)
    {
        var parts = path.Split('/', 2);
        if (parts.Length != 2)
        {
            throw new ArgumentException("Invalid file path format. Expected format: 'bucket/object'.", nameof(path));
        }

        var bucketName = parts[0];
        var objectName = parts[1];

        if (string.IsNullOrWhiteSpace(bucketName) || string.IsNullOrWhiteSpace(objectName))
        {
            throw new ArgumentException("Bucket name and object name must not be empty.", nameof(path));
        }

        try
        {
            var memoryStream = new MemoryStream();
            await client.GetObjectAsync(new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithCallbackStream(stream => stream.CopyTo(memoryStream)));

            memoryStream.Position = 0;
            return memoryStream;
        }
        catch (BucketNotFoundException ex)
        {
            throw new InvalidOperationException($"Bucket '{bucketName}' not found.", ex);
        }
        catch (ObjectNotFoundException ex)
        {
            throw new InvalidOperationException($"Object '{objectName}' not found in bucket '{bucketName}'.", ex);
        }
        catch (MinioException ex)
        {
            throw new InvalidOperationException($"Minio error occurred while retrieving file: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred while retrieving file from S3: {ex.Message}", ex);
        }
    }

    public async Task SaveFileContentAsync(string path, Stream stream, string contentType)
    {
        var parts = path.Split('/', 2);
        if (parts.Length != 2)
        {
            throw new ArgumentException("Invalid file path format. Expected format: 'bucket/object'.", nameof(path));
        }

        var bucketName = parts[0];
        var objectName = parts[1];

        if (string.IsNullOrWhiteSpace(bucketName) || string.IsNullOrWhiteSpace(objectName))
        {
            throw new ArgumentException("Bucket name and object name must not be empty.", nameof(path));
        }

        try
        {
            await client.PutObjectAsync(new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithStreamData(stream)
                .WithContentType(contentType));
        }
        catch (BucketNotFoundException ex)
        {
            throw new InvalidOperationException($"Bucket '{bucketName}' not found.", ex);
        }
        catch (MinioException ex)
        {
            throw new InvalidOperationException($"Minio error occurred while saving file: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred while saving file to S3: {ex.Message}", ex);
        }
    }
}
