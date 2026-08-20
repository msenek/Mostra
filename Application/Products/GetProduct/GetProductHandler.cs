using MediatR;
using Mostra.Application.Interfaces;
using Mostra.Application.Products.CreateProduct;
using Mostra.Domain.Entities;
using Mostra.Application.Products.GetProduct;


namespace Mostra.Application.Products.GetProduct;

public class GetProductHandler : IRequestHandler<ListProductsRequestDto, List<ListProductsResponseDto>>
{
    private readonly IProductRepository _repository;

    public GetProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ListProductsResponseDto>> Handle(ListProductsRequestDto requestDto, CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync(cancellationToken);

        return products.Select(p => new ListProductsResponseDto
        {
            Id = p.Id,
            ProductName = p.ProductName,
            ProductPrice = p.ProductPrice,
            ProductIsInStock = p.ProductIsOnStock
        }).ToList();
    }

}