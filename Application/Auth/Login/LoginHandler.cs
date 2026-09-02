using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mostra.Application.Exceptions;
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mostra.Application.Auth.Login
{
    public class LoginHandler : IRequestHandler<LoginRequestDto, LoginResponseDto>
    {
        private readonly IMerchantRepository _repository;
        private readonly IConfiguration _configuration;

        public LoginHandler(IMerchantRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> Handle(LoginRequestDto request, CancellationToken cancellationToken)
        {
            var merchant = await _repository.GetByEmailAsync(request.Email, cancellationToken);
            if (merchant == null || !BCrypt.Net.BCrypt.Verify(request.Password, merchant.PasswordHash))
                throw new Exception("Invalid credential.");


            var token = GenerateJwtToken(merchant);

            return new LoginResponseDto
            {
                Id = merchant.Id,
                Email = merchant.Email,
                Name = merchant.Name,
                Token = token
            };
        }

        private string GenerateJwtToken(Merchant merchant)
        {
            var secret = _configuration["Jwt:Secret"] ?? "50886510MT808232104201113082010X";
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, merchant.Id.ToString()),
                new Claim(ClaimTypes.Email, merchant.Email),
                new Claim(ClaimTypes.Name, merchant.Name)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}