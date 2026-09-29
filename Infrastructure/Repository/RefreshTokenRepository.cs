using Microsoft.EntityFrameworkCore; 
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;
using Mostra.Infrastructure.Persistence;

namespace Mostra.Infrastructure.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly MostraContext _context;
        public RefreshTokenRepository(MostraContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> CreateRefreshToken(RefreshToken refresh, CancellationToken cancellationToken)
        {
            _context.RefreshTokens.Add(refresh);
            await _context.SaveChangesAsync(cancellationToken);

            return refresh;
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string tokenHash, CancellationToken cancellationToken)
        {
            var refToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.TokenHash == tokenHash, cancellationToken);
            return refToken;
        }

        public async Task RevokeRefreshToken(RefreshToken refreshToken, CancellationToken cancellationToken)
        {
            refreshToken.RevokedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
        }
    } 
}

