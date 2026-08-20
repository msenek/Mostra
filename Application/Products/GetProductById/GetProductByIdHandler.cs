using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;


namespace Mostra.Application.Products.GetProductById
{
    public class GetProductByIdHandler : IRequestHandler<GetProductRequestDto, GetProductResponseDto>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetProductResponseDto> Handle(GetProductRequestDto request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (product == null  )
                throw new KeyNotFoundException("Invalid Id");

            return new GetProductResponseDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                ProductIsOnStock = product.ProductIsOnStock
            };

        }
    }
}
