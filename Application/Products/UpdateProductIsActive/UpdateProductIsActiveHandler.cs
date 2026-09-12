using MediatR;
using Mostra.Application.Catalog.GetPublicCatalog;
using Mostra.Application.Exceptions;
using Mostra.Application.Interfaces;
using Mostra.Application.Products.CreateProduct;
using Mostra.Application.Products.UpdateProduct;

namespace Mostra.Application.Products.UpdateProductIsActive
{
    public class UpdateProductIsActiveHandler : IRequestHandler<UpdateProductIsActiveRequestDto, UpdateProductIsActiveResponseDto>
    {
        private readonly IProductRepository _repository;
        public UpdateProductIsActiveHandler(IProductRepository repository)
        {
            _repository = repository;
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

            return new UpdateProductIsActiveResponseDto()
            {
                IsActive = product.IsActive
            };
        }

    }
}
