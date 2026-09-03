// Application/Businesses/GetBusinessQrCode/GetBusinessQrCodeRequestDto.cs
using MediatR;

namespace Mostra.Application.Businesses.GetBusinessQrCode
{
    public class GetBusinessQrCodeRequestDto : IRequest<byte[]>
    {
        public int BusinessId { get; set; }
    }
}