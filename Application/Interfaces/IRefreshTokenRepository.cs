using Mostra.Domain.Entities;

namespace Mostra.Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> CreateRefreshToken(RefreshToken refresh, CancellationToken cancellationToken);
        Task<RefreshToken?> GetRefreshTokenAsync(string tokenHash, CancellationToken cancellationToken);
        Task RevokeRefreshToken(RefreshToken refreshToken,CancellationToken cancellationToken);

    }
}
