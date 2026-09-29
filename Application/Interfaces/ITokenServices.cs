using Mostra.Domain.Entities;

namespace Mostra.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(Merchant merchant);
        string GenerateRefreshToken();
        string HashRefreshToken(string refreshToken);
    }
}
