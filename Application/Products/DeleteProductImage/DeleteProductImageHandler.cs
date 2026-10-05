using MediatR;
using Mostra.Application.Exceptions;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Products.DeleteProductImage
{
    public class DeleteProductImageHandler : IRequestHandler<DeleteProductImageRequestDto, Unit>
    {
        private readonly IProductRepository _repository;
        private readonly IImageStorageService _imageStorage;
        private readonly ICacheInvalidationService _cacheInvalidation;


        public DeleteProductImageHandler(IProductRepository repository, IImageStorageService imageStorage, ICacheInvalidationService cacheInvalidation)
        {
            _repository = repository;
            _imageStorage = imageStorage;
            _cacheInvalidation = cacheInvalidation;

        }

        public async Task<Unit> Handle(DeleteProductImageRequestDto request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (product == null)
                throw new NotFoundException($"Product with ID {request.Id} not found.");

            if (!string.IsNullOrEmpty(product.ImagePublicId))
            {
                await _imageStorage.DeleteAsync(product.ImagePublicId, cancellationToken);
                product.ImageUrl = null;
                product.ImagePublicId = null;
                await _repository.UpdateProductAsync(product, cancellationToken);
            }

            await _cacheInvalidation.InvalidateCatalogAsync(product.BusinessId, cancellationToken);
            return Unit.Value;
        }
    }
}
