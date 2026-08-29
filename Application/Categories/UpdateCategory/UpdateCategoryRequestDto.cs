using MediatR;

namespace Mostra.Application.Categories.UpdateCategory
{
    public class UpdateCategoryRequestDto : IRequest<UpdateCategoryResponseDto>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}