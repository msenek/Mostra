using MediatR;
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;


namespace Mostra.Application.Products.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductRequestDto, CreateProductResponseDto>
    {
        private readonly IProductRepository _repository;

        public CreateProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateProductResponseDto> Handle(CreateProductRequestDto requestDto, CancellationToken cancellationToken)
        {
            var product = new Product()
            {
                ProductName = requestDto.ProductName,
                ProductDescription = requestDto.ProductDescription,
                ProductPrice = requestDto.ProductPrice,
                ProductIsOnStock = requestDto.ProductIsOnStock,
                BussinesId = requestDto.BusinessId,
                CategoryId = requestDto.CategoryId
            };

            var savedProduct = await _repository.CreateProductAsync(product, cancellationToken);


            var response = new CreateProductResponseDto()
            {
                Id = savedProduct.Id,
                ProductName = savedProduct.ProductName,
                ProductDescription = savedProduct.ProductDescription,
                ProductPrice = savedProduct.ProductPrice,
                ProductIsOnStock = savedProduct.ProductIsOnStock,
                BusinessId = requestDto.BusinessId,
                CategoryId = requestDto.CategoryId
            };

            return response;

        }
    }
}
