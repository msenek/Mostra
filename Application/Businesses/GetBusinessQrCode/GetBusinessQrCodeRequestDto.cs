using MediatR;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Businesses.GetBusinessQrCode
{
    public class GetBusinessQrCodeRequestDto : IRequest<byte[]>, IBusinessOwnedRequest
    {
        public int BusinessId { get; set; }
    }
}