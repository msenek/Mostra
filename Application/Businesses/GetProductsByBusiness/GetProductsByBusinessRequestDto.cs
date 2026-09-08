using MediatR;
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;

namespace Mostra.Application.Products.GetProductsByBusiness
{

    public class GetProductsByBusinessRequestDto : IRequest<List<GetProductsByBusinessResponseDto>>, IBusinessOwnedRequest
    {
        public int BusinessId { get; set; }
    }

   
}