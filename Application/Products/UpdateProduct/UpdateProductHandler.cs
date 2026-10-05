using MediatR;
using Mostra.Application.Exceptions;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Products.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductRequestDto, UpdateProductResponseDto>
    {
        private readonly IProductRepository _repository;
        private readonly ICacheInvalidationService _cacheInvalidation;

        public UpdateProductHandler(IProductRepository repository, ICacheInvalidationService cacheInvalidation)
        {
            _repository = repository;
            _cacheInvalidation = cacheInvalidation;
        }

        public async Task<UpdateProductResponseDto> Handle(UpdateProductRequestDto requestDto, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(requestDto.Id, cancellationToken);

            if (product == null)
                throw new NotFoundException($"Product with ID {requestDto.Id} not found.");

            if (requestDto.ProductName != null)
                product.ProductName = requestDto.ProductName;

            if (requestDto.ProductDescription != null)
                product.ProductDescription = requestDto.ProductDescription;

            if (requestDto.ProductPrice.HasValue)
                product.ProductPrice = requestDto.ProductPrice.Value;

            if (requestDto.ProductIsOnStock.HasValue)
                product.ProductIsOnStock = requestDto.ProductIsOnStock.Value;

            if (requestDto.IsActive.HasValue)
                product.IsActive = requestDto.IsActive.Value;


            await _repository.UpdateProductAsync(product, cancellationToken);

            await _cacheInvalidation.InvalidateCatalogAsync(product.BusinessId, cancellationToken);

            return new UpdateProductResponseDto()
            {
                Id = product.Id,
                ProductName = product.ProductName ?? string.Empty,
                ProductDescription = product.ProductDescription ?? string.Empty,
                ProductPrice = product.ProductPrice,
                ProductIsOnStock = product.ProductIsOnStock,
                IsActive = product.IsActive
            };
        }
    }
}