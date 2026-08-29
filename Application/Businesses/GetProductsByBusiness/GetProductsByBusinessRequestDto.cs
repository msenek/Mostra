using MediatR;
using Mostra.Domain.Entities;

namespace Mostra.Application.Products.GetProductsByBusiness
{

    public class GetProductsByBusinessRequestDto : IRequest<List<GetProductsByBusinessResponseDto>>
    {
        public int BusinessId { get; set; }
    }

   
}