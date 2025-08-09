using GroovE.Application.Content;

namespace GroovE.Infrastructure.Content.FileService;

internal class FileContentService : IContentService
{
    public Task<Stream> GetFileContentAsync(string path)
    {
        try
        {
            var fullPath = Path.GetFullPath(path, AppContext.BaseDirectory);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"File not found: {fullPath}");

            var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);
            return Task.FromResult<Stream>(stream);
        }
        catch (Exception ex)
        {
            throw new IOException($"Failed to read file '{path}': {ex.Message}", ex);
        }
    }

    public async Task SaveFileContentAsync(string path, Stream stream, string contentType)
    {
        try
        {
            var fullPath = Path.GetFullPath(path, AppContext.BaseDirectory);
            var directory = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory!);

            using var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true);
            await stream.CopyToAsync(fileStream);
        }
        catch (Exception ex)
        {
            throw new IOException($"Failed to save file '{path}': {ex.Message}", ex);
        }
    }
}
