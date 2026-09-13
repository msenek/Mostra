namespace Mostra.Application.Interfaces
{
    public interface IImageStorageService
    {
        Task<string> UploadAsync(IFormFile file, string fileName, CancellationToken cancellationToken = default);
        Task DeleteAsync(string publicId, CancellationToken cancellationToken = default);
    }
}
