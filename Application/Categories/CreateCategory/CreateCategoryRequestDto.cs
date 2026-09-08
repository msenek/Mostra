using MediatR;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Categories.CreateCategory
{
    public class CreateCategoryRequestDto : IRequest<CreateCategoryResponseDto>, IBusinessOwnedRequest
    {
        public int BusinessId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

    }
}