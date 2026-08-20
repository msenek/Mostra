using MediatR;
using Mostra.Application.Products.CreateProduct;

namespace Mostra.Application.Products.GetProduct
{
    public class ListProductsRequestDto : IRequest<List<ListProductsResponseDto>>
    {

    }
    
}
