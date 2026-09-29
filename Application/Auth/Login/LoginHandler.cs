using MediatR;
using Mostra.Application.Exceptions;
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;

namespace Mostra.Application.Auth.Login
{
    public class LoginHandler : IRequestHandler<LoginRequestDto, LoginResponseDto>
    {
        private readonly IMerchantRepository _repository;
        private readonly IRefreshTokenRepository _refreshToken;
        private readonly ITokenService _tokenService;

        public LoginHandler(IMerchantRepository repository, IRefreshTokenRepository refreshToken, ITokenService tokenService)
        {
            _repository = repository;
            _refreshToken = refreshToken;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto> Handle(LoginRequestDto request, CancellationToken cancellationToken)
        {
            // get merchant
            var merchant = await _repository.GetByEmailAsync(request.Email, cancellationToken);

            if (merchant == null)
                throw new UnauthorizedException("Invalid credential.");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, merchant.PasswordHash))
                throw new UnauthorizedException("Invalid credential.");

            // create access token
            var token = _tokenService.GenerateAccessToken(merchant);

            // create refresh token
            var refreshToken = _tokenService.GenerateRefreshToken();

            // save refresh token with hash
            var refreshTokenEntity = new RefreshToken
            {
                MerchantId = merchant.Id,
                TokenHash = _tokenService.HashRefreshToken(refreshToken),
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            await _refreshToken.CreateRefreshToken(refreshTokenEntity, cancellationToken);

            return new LoginResponseDto
            {
                Id = merchant.Id,
                Email = merchant.Email,
                Name = merchant.Name,
                Token = token,
                RefreshToken = refreshToken
            };
        }
    }
}