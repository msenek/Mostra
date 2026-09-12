using MediatR;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Categories.DeleteCategory
{
    public class DeleteCategoryRequestDto : IRequest<Unit>, ICategoryOwnedRequest
    {
        public int Id { get; set; }
        public int CategoryId => Id;
    }
}