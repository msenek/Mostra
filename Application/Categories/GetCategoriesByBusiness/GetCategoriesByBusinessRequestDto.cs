using MediatR;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Categories.GetCategoriesByBusiness
{
    public class GetCategoriesByBusinessRequestDto : IRequest<List<GetCategoriesByBusinessResponseDto>>, IBusinessOwnedRequest
    {
        public int BusinessId { get; set; }
    }
}