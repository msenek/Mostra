using MediatR;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Categories.UpdateCategory
{
    public class UpdateCategoryRequestDto : IRequest<UpdateCategoryResponseDto>, ICategoryOwnedRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId => Id;
    }
}