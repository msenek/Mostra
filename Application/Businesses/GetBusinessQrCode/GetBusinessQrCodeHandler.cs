// Application/Businesses/GetBusinessQrCode/GetBusinessQrCodeHandler.cs
using MediatR;
using Microsoft.Extensions.Configuration;
using Mostra.Application.Exceptions;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Businesses.GetBusinessQrCode
{
    public class GetBusinessQrCodeHandler : IRequestHandler<GetBusinessQrCodeRequestDto, byte[]>
    {
        private readonly IBusinessRepository _repository;
        private readonly ICurrentUserService _currentUser;
        private readonly IQrCodeGenerator _qrCodeGenerator;
        private readonly IConfiguration _configuration;

        public GetBusinessQrCodeHandler(
            IBusinessRepository repository,
            ICurrentUserService currentUser,
            IQrCodeGenerator qrCodeGenerator,
            IConfiguration configuration)
        {
            _repository = repository;
            _currentUser = currentUser;
            _qrCodeGenerator = qrCodeGenerator;
            _configuration = configuration;
        }

        public async Task<byte[]> Handle(GetBusinessQrCodeRequestDto request, CancellationToken cancellationToken)
        {
            var business = await _repository.GetByIdAsync(request.BusinessId, cancellationToken)
                ?? throw new NotFoundException("Business not found.");

            if (business.MerchantId != _currentUser.MerchantId)
                throw new ForbiddenException("No tenés permiso sobre este negocio.");

            var baseUrl = _configuration["PublicCatalog:BaseUrl"]
                ?? throw new InvalidOperationException("Falta configurar PublicCatalog:BaseUrl.");

            var catalogUrl = $"{baseUrl}/api/catalog/{business.UniqueSlug}";

            return _qrCodeGenerator.GeneratePng(catalogUrl);
        }
    }
}