using MediatR;
using Mostra.Application.Exceptions;
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;

namespace Mostra.Application.Businesses.UpdateBusiness
{
    public class UpdateBusinessHandler : IRequestHandler<UpdateBusinessRequestDto, UpdateBusinessResponseDto>
    {
        private readonly IBusinessRepository _repository;

        public UpdateBusinessHandler(IBusinessRepository repository) => _repository = repository;

        public async Task<UpdateBusinessResponseDto> Handle(UpdateBusinessRequestDto request, CancellationToken cancellationToken)
        {
            var business = await _repository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException("Business not found."); 

            if (!string.IsNullOrWhiteSpace(request.Name)) business.Name = request.Name;
            if (request.Description != null) business.Description = request.Description;
            if (request.LogoUrl != null) business.LogoUrl = request.LogoUrl;

            var updated = await _repository.UpdateAsync(business, cancellationToken);

            return new UpdateBusinessResponseDto
            {
                Id = updated.Id,
                Name = updated.Name,
                Description = updated.Description,
                LogoUrl = updated.LogoUrl,
                UniqueSlug = updated.UniqueSlug,
                
            };
        }
    }
}