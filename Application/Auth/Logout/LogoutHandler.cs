using MediatR;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Auth.Logout
{
    public class LogoutHandler : IRequestHandler<LogoutRequestDto, Unit>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenService _tokenService;

        public LogoutHandler(IRefreshTokenRepository refreshTokenRepository, ITokenService tokenService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _tokenService = tokenService;
        }

        public async Task<Unit> Handle(LogoutRequestDto request, CancellationToken cancellationToken)
        {
            var tokenHash = _tokenService.HashRefreshToken(request.Token);
            var storedToken = await _refreshTokenRepository.GetRefreshTokenAsync(tokenHash, cancellationToken);

            if (storedToken != null && storedToken.RevokedAt == null)
            {
                await _refreshTokenRepository.RevokeRefreshToken(storedToken, cancellationToken);
            }

            return Unit.Value;
        }
    }
}