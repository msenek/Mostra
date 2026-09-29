using MediatR;
using Mostra.Application.Auth.Login;

namespace Mostra.Application.Auth.Refresh
{
    public class RefreshTokenRequestDto : IRequest<RefreshTokenResponseDto>
    {
        public string? RefreshToken { get; set; }
    }
}
