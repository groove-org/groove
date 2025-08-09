namespace GroovE.Application.Content;

/// <summary>
/// Provides methods for retrieving and saving file content.
/// </summary>
public interface IContentService
{
    /// <summary>
    /// Asynchronously retrieves the content of a file as a stream.
    /// </summary>
    /// <param name="path">The path to the file.</param>
    /// <returns>A <see cref="Task{Stream}"/> representing the asynchronous operation, with the file content as a stream.</returns>
    Task<Stream> GetFileContentAsync(string path);

    /// <summary>
    /// Asynchronously saves the provided stream as file content.
    /// </summary>
    /// <param name="path">The path where the file will be saved.</param>
    /// <param name="stream">The stream containing the file content.</param>
    /// <param name="contentType">The MIME type of the content.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task SaveFileContentAsync(string path, Stream stream, string contentType);
}
