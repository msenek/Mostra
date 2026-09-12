using MediatR;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Products.DeleteProduct
{
    public class DeleteProductRequestDto : IRequest<Unit>, IProductOwnedRequest
    {
        public int Id { get; set; }
        public int ProductId => Id;
    }
}
