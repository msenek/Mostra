using MediatR;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Catalog.GetPublicCatalog
{
    public class GetPublicCatalogRequestDto : IRequest<GetPublicCatalogResponseDto>, ICacheableRequest
    {
        public string Slug { get; set; } = string.Empty;
        public string CacheKey => $"catalog:{Slug}";
        public TimeSpan? Expiration => TimeSpan.FromMinutes(5);

    }
}