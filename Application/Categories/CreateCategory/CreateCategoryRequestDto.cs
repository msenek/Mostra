using MediatR;

namespace Mostra.Application.Categories.CreateCategory
{
    public class CreateCategoryRequestDto : IRequest<CreateCategoryResponseDto>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int BusinessId { get; set; } 
    }
}