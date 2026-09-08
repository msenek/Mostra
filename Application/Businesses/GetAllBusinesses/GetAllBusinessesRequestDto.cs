using MediatR;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Businesses.GetAllBusinesses
{
    public class GetAllBusinessesRequestDto : IRequest<List<GetAllBusinessesResponseDto>>
    {
    }
}