using MediatR;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Products.DeleteProductImage
{
    public class DeleteProductImageRequestDto : IRequest<Unit>, IProductOwnedRequest
    {
        public int Id { get; set; }

        public int ProductId => Id;
    }
}