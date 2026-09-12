using MediatR;
using Mostra.Application.Common.Behaviors;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Products.UpdateProductIsActive
{
    public class UpdateProductIsActiveRequestDto : IRequest<UpdateProductIsActiveResponseDto>, IProductOwnedRequest
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int ProductId => Id;
    }
}
