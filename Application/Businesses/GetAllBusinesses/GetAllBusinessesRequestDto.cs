using MediatR;

namespace Mostra.Application.Businesses.GetAllBusinesses
{
    public class GetAllBusinessesRequestDto : IRequest<List<GetAllBusinessesResponseDto>>
    {
    }
}