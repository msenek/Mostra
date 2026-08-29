using MediatR;

namespace Mostra.Application.Businesses.DeleteBusiness
{
    public class DeleteBusinessRequestDto : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}