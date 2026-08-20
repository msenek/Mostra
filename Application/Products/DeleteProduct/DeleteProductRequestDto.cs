using MediatR;

namespace Mostra.Application.Products.DeleteProduct
{
    public class DeleteProductRequestDto : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
