using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Mostra.Application.Interfaces;

public class CloudinaryImageStorageService : IImageStorageService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryImageStorageService(IConfiguration configuration)
    {
        var account = new Account(
            configuration["Cloudinary:CloudName"],
            configuration["Cloudinary:ApiKey"],
            configuration["Cloudinary:ApiSecret"]
        );
        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> UploadAsync(IFormFile file, string fileName, CancellationToken cancellationToken = default)
    {
        await using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(fileName, stream),
            PublicId = fileName,
            Folder = "mostra/products"
        };

        var result = await _cloudinary.UploadAsync(uploadParams, cancellationToken);
        return result.SecureUrl.ToString();
    }

    public async Task DeleteAsync(string publicId, CancellationToken cancellationToken = default)
    {
        var deleteParams = new DeletionParams(publicId);
        await _cloudinary.DestroyAsync(deleteParams);
    }
}