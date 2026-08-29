using MediatR;

namespace Mostra.Application.Categories.GetCategoriesByBusiness
{
    public class GetCategoriesByBusinessRequestDto : IRequest<List<GetCategoriesByBusinessResponseDto>>
    {
        public int BusinessId { get; set; }
    }
}