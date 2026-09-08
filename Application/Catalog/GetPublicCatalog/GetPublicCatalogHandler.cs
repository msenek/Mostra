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
                ?? throw new NotFoundException("Catalog not found");

            var category = business.Categories
                .Select(c => new PublicCategoryDto
                {
                    Name = c.Name,
                    Products = business.Products
                        .Where(p => p.CategoryId == c.Id)
                        .Select(MapProduct)
                        .ToList()
                })
                .ToList();

            // product sin category asig CategoryId == null van en un grupo apart.
            var withoutCategory = business.Products
                .Where(p => p.CategoryId == null)
                .Select(MapProduct)
                .ToList();

            if (withoutCategory.Count > 0)
            {
                category.Add(new PublicCategoryDto { Name = "Otros", Products = withoutCategory });
            }

            return new GetPublicCatalogResponseDto
            {
                BusinessName = business.Name,
                Description = business.Description,
                LogoUrl = business.LogoUrl,
                Categories = category
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
    