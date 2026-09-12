using MediatR;
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;
using BCrypt.Net;
using Mostra.Application.Exceptions;

namespace Mostra.Application.Auth.Register
{
    public class RegisterHandler : IRequestHandler<RegisterRequestDto, RegisterResponseDto>
    {
        private readonly IMerchantRepository _repository;

        public RegisterHandler(IMerchantRepository repository) => _repository = repository;

        public async Task<RegisterResponseDto> Handle(RegisterRequestDto request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByEmailAsync(request.Email, cancellationToken);
            if (existing != null)
                throw new ConflictException("The email already exists");

            var merchant = new Merchant
            {
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Name = request.Name
            };

            var saved = await _repository.CreateAsync(merchant, cancellationToken);

            return new RegisterResponseDto
            {
                Id = saved.Id,
                Email = saved.Email,
                Name = saved.Name
            };
        }
    }
}