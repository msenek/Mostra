using MediatR;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Businesses.GetAllBusinesses
{
    public class GetAllBusinessesHandler : IRequestHandler<GetAllBusinessesRequestDto, List<GetAllBusinessesResponseDto>>
    {
        private readonly IBusinessRepository _repository;

        public GetAllBusinessesHandler(IBusinessRepository repository) => _repository = repository;

        public async Task<List<GetAllBusinessesResponseDto>> Handle(GetAllBusinessesRequestDto request, CancellationToken cancellationToken)
        {
            var businesses = await _repository.GetAllAsync(cancellationToken);

            return businesses.Select(b => new GetAllBusinessesResponseDto
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                LogoUrl = b.LogoUrl,
                UniqueSlug = b.UniqueSlug
            }).ToList();
        }
    }
}