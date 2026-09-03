using MediatR;

namespace Mostra.Application.Catalog.GetPublicCatalog
{
    public class GetPublicCatalogRequestDto : IRequest<GetPublicCatalogResponseDto>
    {
        public string Slug { get; set; } = string.Empty;
    }
}