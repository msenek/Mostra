using MediatR;

namespace Mostra.Application.Auth.Logout
{
    public class LogoutRequestDto : IRequest<Unit>
    {
        public string Token { get; set; } = string.Empty;
    }
}