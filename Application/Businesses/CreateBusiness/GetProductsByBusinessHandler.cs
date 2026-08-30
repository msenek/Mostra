using MediatR;
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;

namespace Mostra.Application.Products.GetProductsByBusiness
{
    public class GetProductsByBusinessHandler : IRequestHandler<GetProductsByBusinessRequestDto, List<GetProductsByBusinessResponseDto>>
    {
        private readonly IProductRepository _repository;

        public GetProductsByBusinessHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetProductsByBusinessResponseDto>> Handle(GetProductsByBusinessRequestDto request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllByBusinessIdAsync(request.BusinessId, cancellationToken);


            return products.Select(p => new GetProductsByBusinessResponseDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                ProductPrice = p.ProductPrice,
                ProductIsOnStock = p.ProductIsOnStock,
                BusinessId = (int)p.BusinessId,
                CategoryId = (int)p.CategoryId
            }).ToList();
        }
    }
}