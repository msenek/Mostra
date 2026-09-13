using MediatR;
using Mostra.Application.Exceptions;
using Mostra.Application.Interfaces;
using Mostra.Application.Products.UpdateProductImage;

public class UpdateProductImageHandler : IRequestHandler<UpdateProductImageRequestDto, UpdateProductImageResponseDto>
{
    private readonly IProductRepository _repository;
    private readonly IImageStorageService _imageStorage;

    public UpdateProductImageHandler(IProductRepository repository, IImageStorageService imageStorage)
    {
        _repository = repository;
        _imageStorage = imageStorage;
    }

    public async Task<UpdateProductImageResponseDto> Handle(UpdateProductImageRequestDto request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new NotFoundException($"Product with ID {request.Id} not found.");

        if (!string.IsNullOrEmpty(product.ImagePublicId))
        {
            await _imageStorage.DeleteAsync(product.ImagePublicId, cancellationToken);
        }

        var fileName = $"product-{request.Id}-{Guid.NewGuid()}";
        var imageUrl = await _imageStorage.UploadAsync(request.File, fileName, cancellationToken);

        product.ImageUrl = imageUrl;
        product.ImagePublicId = fileName;
        await _repository.UpdateProductAsync(product, cancellationToken);

        return new UpdateProductImageResponseDto
        { 
            ImageUrl = imageUrl 
        };


    }
}