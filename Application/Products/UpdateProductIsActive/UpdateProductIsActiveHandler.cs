using MediatR;
using Mostra.Application.Exceptions;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Products.UpdateProductIsActive
{
    public class UpdateProductIsActiveHandler : IRequestHandler<UpdateProductIsActiveRequestDto, UpdateProductIsActiveResponseDto>
    {
        private readonly IProductRepository _repository;
        private readonly ICacheInvalidationService _cacheInvalidation;

        public UpdateProductIsActiveHandler(IProductRepository repository, ICacheInvalidationService cacheInvalidation)
        {
            _repository = repository;
            _cacheInvalidation = cacheInvalidation;
        }

        public async Task<UpdateProductIsActiveResponseDto> Handle(UpdateProductIsActiveRequestDto requestDto, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(requestDto.Id, cancellationToken);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID {requestDto.Id} not found.");
            }

            if (product.IsDeleted)
            {
                throw new ConflictException("You can't active a deleted product.");
            }
            product.IsActive = requestDto.IsActive;

            await _repository.UpdateProductAsync(product, cancellationToken);

            await _cacheInvalidation.InvalidateCatalogAsync(product.BusinessId, cancellationToken);

            return new UpdateProductIsActiveResponseDto()
            {
                IsActive = product.IsActive
            };
        }
    }
}