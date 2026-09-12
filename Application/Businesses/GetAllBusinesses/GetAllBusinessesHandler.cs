using MediatR;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Businesses.GetAllBusinesses
{
    public class GetAllBusinessesHandler : IRequestHandler<GetAllBusinessesRequestDto, List<GetAllBusinessesResponseDto>>
    {
        private readonly IBusinessRepository _repository;
        private readonly ICurrentUserService _currentUser;

        public GetAllBusinessesHandler(IBusinessRepository repository, ICurrentUserService currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public async Task<List<GetAllBusinessesResponseDto>> Handle(GetAllBusinessesRequestDto request, CancellationToken cancellationToken)
        {

            var merchantId = _currentUser.MerchantId;
            var businesses = await _repository.GetAllAsync(merchantId, cancellationToken);
           
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