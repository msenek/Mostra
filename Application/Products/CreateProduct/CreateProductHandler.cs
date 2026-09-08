using MediatR;
using Mostra.Application.Exceptions;
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;


namespace Mostra.Application.Products.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductRequestDto, CreateProductResponseDto>
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        public CreateProductHandler(IProductRepository repository, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _repository = repository;
        }

        public async Task<CreateProductResponseDto> Handle(CreateProductRequestDto requestDto, CancellationToken cancellationToken)
        {

            var category = await _categoryRepository.GetByIdAsync(requestDto.CategoryId);

            if (category == null)
            {
                throw new NotFoundException("La categoría indicada no existe.");
            }

            if (category.BusinessId != requestDto.BusinessId)
            {
                throw new ForbiddenException("the category doesn't belong to this business");
            }
            
            var product = new Product()
            {
                ProductName = requestDto.ProductName,
                ProductDescription = requestDto.ProductDescription,
                ProductPrice = requestDto.ProductPrice,
                ProductIsOnStock = requestDto.ProductIsOnStock,
                BusinessId = requestDto.BusinessId,
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
