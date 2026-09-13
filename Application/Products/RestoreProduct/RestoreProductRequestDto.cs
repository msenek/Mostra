using MediatR;

namespace Mostra.Application.Products.RestoreProduct
{
    public class RestoreProductRequestDto : IRequest<RestoreProductResponseDto>
    {
        public int Id { get; set; }
    }
}