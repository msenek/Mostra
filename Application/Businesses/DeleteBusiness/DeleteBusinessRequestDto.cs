using MediatR;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Businesses.DeleteBusiness
{
    public class DeleteBusinessRequestDto : IRequest<Unit>, IBusinessOwnedRequest
    {
        public int Id { get; set; }
        public int BusinessId => Id;
    }
}