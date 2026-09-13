using MediatR;
using Mostra.Application.Exceptions;
using Mostra.Application.Interfaces;
using Mostra.Application.Products.UploadImageToProduct;

public class UploadProductImageHandler : IRequestHandler<UploadProductImageRequestDto, UploadProductImageResponseDto>
{
    private readonly IProductRepository _repository;
    private readonly IImageStorageService _imageStorage;

    public UploadProductImageHandler(IProductRepository repository, IImageStorageService imageStorage)
    {
        _repository = repository;
        _imageStorage = imageStorage;
    }

    public async Task<UploadProductImageResponseDto> Handle(UploadProductImageRequestDto request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new NotFoundException($"Product with ID {request.Id} not found.");

        var fileName = $"product-{request.Id}-{Guid.NewGuid()}";
        var imageUrl = await _imageStorage.UploadAsync(request.File, fileName, cancellationToken);
      

        product.ImageUrl = imageUrl;
        product.ImagePublicId = fileName;

        await _repository.UpdateProductAsync(product, cancellationToken);

        return new UploadProductImageResponseDto { ImageUrl = imageUrl };
    }
}