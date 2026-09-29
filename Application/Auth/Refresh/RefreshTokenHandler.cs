using MediatR;
using Mostra.Application.Auth.Login;
using Mostra.Application.Exceptions;
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;

namespace Mostra.Application.Auth.Refresh
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequestDto, RefreshTokenResponseDto>
    {
        private readonly IMerchantRepository _repository;
        private readonly IRefreshTokenRepository _refreshRepository;
        private readonly ITokenService _tokenService;

        public RefreshTokenHandler(IMerchantRepository repository, IRefreshTokenRepository refreshRepository, ITokenService tokenService)
        {
            _repository = repository;
            _refreshRepository = refreshRepository;
            _tokenService = tokenService;

        }

        public async Task<RefreshTokenResponseDto> Handle(RefreshTokenRequestDto dto, CancellationToken cancellationToken)
        {
            var tokenHash = _tokenService.HashRefreshToken(dto.RefreshToken);
            var refToken = await _refreshRepository.GetRefreshTokenAsync(tokenHash, cancellationToken);
            if (refToken == null)
                throw new UnauthorizedException("Invalid refresh token.");
            if (refToken.RevokedAt != null)
                throw new UnauthorizedException("Refresh token has been revoked.");
            if (refToken.ExpiresAt <= DateTime.UtcNow)
                throw new UnauthorizedException("Refresh token has expired.");

            var merchantId = refToken.MerchantId;
            var merchant = await _repository.GetByIdAsync(merchantId, cancellationToken);
            if (merchant == null)
                throw new UnauthorizedException("Merchant not found.");

            var newAccessToken = _tokenService.GenerateAccessToken(merchant);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            await _refreshRepository.RevokeRefreshToken(refToken, cancellationToken);
            var newRefreshTokenEntity = new RefreshToken
            {
                MerchantId = merchant.Id,
                TokenHash = _tokenService.HashRefreshToken(newRefreshToken),
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            await _refreshRepository.CreateRefreshToken(newRefreshTokenEntity, cancellationToken);

            return new RefreshTokenResponseDto
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            };

        }
    }
}
