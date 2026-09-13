using MediatR;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Products.RestoreProduct
{
    public class RestoreProductRequestDto : IRequest<RestoreProductResponseDto>, IProductOwnedRequest
    {
        public int Id { get; set; }
        public int ProductId => Id;
    }
}