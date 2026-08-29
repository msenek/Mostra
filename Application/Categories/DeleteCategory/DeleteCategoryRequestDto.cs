using MediatR;

namespace Mostra.Application.Categories.DeleteCategory
{
    public class DeleteCategoryRequestDto : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}