using MediatR;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Products.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductRequestDto, UpdateProductResponseDto>
    {
        private readonly IProductRepository _repository;

        public UpdateProductHandler(IProductRepository repository) => _repository = repository;

        public async Task<UpdateProductResponseDto> Handle(UpdateProductRequestDto requestDto, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(requestDto.Id, cancellationToken);

            if (product == null)
                throw new KeyNotFoundException($"Producto con ID {requestDto.Id} no encontrado.");

            if (requestDto.ProductName != null)
                product.ProductName = requestDto.ProductName;

            if (requestDto.ProductDescription != null)
                product.ProductDescription = requestDto.ProductDescription;

            if (requestDto.ProductPrice.HasValue)
                product.ProductPrice = requestDto.ProductPrice.Value;

            if (requestDto.ProductIsOnStock.HasValue)
                product.ProductIsOnStock = requestDto.ProductIsOnStock.Value;


            await _repository.UpdateProductAsync(product, cancellationToken);

            return new UpdateProductResponseDto()
            {
                Id = product.Id,
                ProductName = product.ProductName ?? string.Empty,
                ProductDescription = product.ProductDescription ?? string.Empty,
                ProductPrice = product.ProductPrice,
                ProductIsOnStock = product.ProductIsOnStock
            };
        }
    }
}