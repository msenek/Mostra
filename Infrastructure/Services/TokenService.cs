using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Mostra.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // refresh generate
        public string GenerateRefreshToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);

            return Convert.ToBase64String(randomBytes);
        }

        // refresh hash generate
        public string HashRefreshToken(string refreshToken)
        {
            var bytes = SHA256.HashData(
                Encoding.UTF8.GetBytes(refreshToken));

            return Convert.ToBase64String(bytes);
        }

        // access token generate
        public string GenerateAccessToken(Merchant merchant)
        {
            var secret = _configuration["Jwt:Secret"];

            var key = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(secret));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    merchant.Id.ToString()),

                new Claim(
                    ClaimTypes.Email,
                    merchant.Email),

                new Claim(
                    ClaimTypes.Name,
                    merchant.Name)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}