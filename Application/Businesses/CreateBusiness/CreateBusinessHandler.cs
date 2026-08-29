using MediatR;
using Mostra.Application.Bussines.CreateBussines;
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;
using System.Text.RegularExpressions;

namespace Mostra.Application.Businesses.CreateBusiness
{
    public class CreateBusinessHandler : IRequestHandler<CreateBusinessRequestDto, CreateBusinessResponseDto>
    {
        private readonly IBusinessRepository _businessRepository;

        public CreateBusinessHandler(IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;
        }

        public async Task<CreateBusinessResponseDto> Handle(CreateBusinessRequestDto request, CancellationToken cancellationToken)
        {
            var slug = GenerateSlug(request.Name);
            var business = new Business
            {
                Name = request.Name,
                Description = request.Description,
                LogoUrl = request.LogoUrl,
                UniqueSlug = slug
            };
            var savedBusiness = await _businessRepository.CreateAsync(business, cancellationToken);

            return new CreateBusinessResponseDto
            {
                Id = savedBusiness.Id,
                Name = savedBusiness.Name,
                UniqueSlug = savedBusiness.UniqueSlug
            };
        }
        private string GenerateSlug(string text)
        {
            var lower = text.ToLowerInvariant();
            var withoutAccents = lower.Normalize(System.Text.NormalizationForm.FormD)
                                  .Where(c => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                                  .ToArray();
            var clean = new string(withoutAccents).Replace("ñ", "n");
            return Regex.Replace(clean, @"[^a-z0-9\s-]", "").Replace(" ", "-");
        }
    }
}