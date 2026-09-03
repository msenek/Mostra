using MediatR;
using Mostra.Application.Exceptions;
using Mostra.Application.Interfaces;
using Mostra.Application.Catalog.GetPublicCatalog;

namespace Mostra.Application.Catalog.GetPublicCatalog
{
    public class GetPublicCatalogHandler : IRequestHandler<GetPublicCatalogRequestDto, GetPublicCatalogResponseDto>
    {
        private readonly IBusinessRepository _repository;

        public GetPublicCatalogHandler(IBusinessRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetPublicCatalogResponseDto> Handle(GetPublicCatalogRequestDto request, CancellationToken cancellationToken)
        {
            var business = await _repository.GetBySlugWithCatalogAsync(request.Slug, cancellationToken)
                ?? throw new NotFoundException("Catálogo no encontrado.");

            var categorias = business.Categories
                .Select(c => new PublicCategoryDto
                {
                    Name = c.Name,
                    Products = business.Products
                        .Where(p => p.CategoryId == c.Id)
                        .Select(MapProduct)
                        .ToList()
                })
                .ToList();

            // p sin cat asig CategoryId == null van en un grupo apart.
            var sinCategoria = business.Products
                .Where(p => p.CategoryId == null)
                .Select(MapProduct)
                .ToList();

            if (sinCategoria.Count > 0)
            {
                categorias.Add(new PublicCategoryDto { Name = "Otros", Products = sinCategoria });
            }

            return new GetPublicCatalogResponseDto
            {
                BusinessName = business.Name,
                Description = business.Description,
                LogoUrl = business.LogoUrl,
                Categories = categorias
            };
        }

        private static PublicProductDto MapProduct(Mostra.Domain.Entities.Product p) => new()
        {
            Id = p.Id,
            ProductName = p.ProductName ?? string.Empty,
            ProductDescription = p.ProductDescription,
            ProductPrice = p.ProductPrice,
            ProductIsOnStock = p.ProductIsOnStock
        };
    }
}
    