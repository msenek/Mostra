using Moq;
using Mostra.Application.Auth.Refresh;
using Mostra.Application.Exceptions;
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;
using Xunit;

namespace Mostra.Tests.Auth.Refresh
{
    public class RefreshTokenHandlerShould
    {
        private readonly Mock<IMerchantRepository> _merchantRepo = new();
        private readonly Mock<IRefreshTokenRepository> _refreshRepo = new();
        private readonly Mock<ITokenService> _tokenService = new();

        private RefreshTokenHandler CreateHandler() =>
            new(_merchantRepo.Object, _refreshRepo.Object, _tokenService.Object);

        [Fact]
        public async Task RotateTokens_WhenRefreshTokenIsValid()
        {
            var merchant = new Merchant { Id = 1, Email = "test@mostra.com", Name = "Test" };
            var stored = new RefreshToken
            {
                MerchantId = 1,
                TokenHash = "hash-old",
                ExpiresAt = DateTime.UtcNow.AddDays(1),
                RevokedAt = null
            };

            _tokenService.Setup(t => t.HashRefreshToken("raw-old")).Returns("hash-old");
            _refreshRepo.Setup(r => r.GetRefreshTokenAsync("hash-old", It.IsAny<CancellationToken>()))
                .ReturnsAsync(stored);
            _merchantRepo.Setup(m => m.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(merchant);
            _tokenService.Setup(t => t.GenerateAccessToken(merchant)).Returns("new-access");
            _tokenService.Setup(t => t.GenerateRefreshToken()).Returns("raw-new");
            _tokenService.Setup(t => t.HashRefreshToken("raw-new")).Returns("hash-new");

            var result = await CreateHandler().Handle(
                new RefreshTokenRequestDto { RefreshToken = "raw-old" },
                CancellationToken.None);

            Assert.Equal("new-access", result.Token);
            Assert.Equal("raw-new", result.RefreshToken);
            _refreshRepo.Verify(r => r.RevokeRefreshToken(stored, It.IsAny<CancellationToken>()), Times.Once);
            _refreshRepo.Verify(r => r.CreateRefreshToken(
                It.Is<RefreshToken>(t => t.TokenHash == "hash-new"),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ThrowUnauthorized_WhenTokenDoesNotExist()
        {
            _tokenService.Setup(t => t.HashRefreshToken(It.IsAny<string>())).Returns("hash");
            _refreshRepo.Setup(r => r.GetRefreshTokenAsync("hash", It.IsAny<CancellationToken>()))
                .ReturnsAsync((RefreshToken?)null);

            await Assert.ThrowsAsync<UnauthorizedException>(() =>
                CreateHandler().Handle(
                    new RefreshTokenRequestDto { RefreshToken = "anything" },
                    CancellationToken.None));
        }

        [Fact]
        public async Task ThrowUnauthorized_WhenTokenIsRevoked()
        {
            var stored = new RefreshToken
            {
                MerchantId = 1,
                TokenHash = "hash",
                ExpiresAt = DateTime.UtcNow.AddDays(1),
                RevokedAt = DateTime.UtcNow.AddMinutes(-5)
            };

            _tokenService.Setup(t => t.HashRefreshToken(It.IsAny<string>())).Returns("hash");
            _refreshRepo.Setup(r => r.GetRefreshTokenAsync("hash", It.IsAny<CancellationToken>()))
                .ReturnsAsync(stored);

            await Assert.ThrowsAsync<UnauthorizedException>(() =>
                CreateHandler().Handle(
                    new RefreshTokenRequestDto { RefreshToken = "anything" },
                    CancellationToken.None));
        }

        [Fact]
        public async Task ThrowUnauthorized_WhenTokenIsExpired()
        {
            var stored = new RefreshToken
            {
                MerchantId = 1,
                TokenHash = "hash",
                ExpiresAt = DateTime.UtcNow.AddDays(-1),
                RevokedAt = null
            };

            _tokenService.Setup(t => t.HashRefreshToken(It.IsAny<string>())).Returns("hash");
            _refreshRepo.Setup(r => r.GetRefreshTokenAsync("hash", It.IsAny<CancellationToken>()))
                .ReturnsAsync(stored);

            await Assert.ThrowsAsync<UnauthorizedException>(() =>
                CreateHandler().Handle(
                    new RefreshTokenRequestDto { RefreshToken = "anything" },
                    CancellationToken.None));
        }
    }
}