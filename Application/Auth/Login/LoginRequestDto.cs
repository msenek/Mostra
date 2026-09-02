using MediatR;

namespace Mostra.Application.Auth.Login
{
    public class LoginRequestDto : IRequest<LoginResponseDto>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}