namespace Mostra.Application.Catalog.GetPublicCatalog
{
    public class GetPublicCatalogResponseDto
    {
        public string BusinessName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? LogoUrl { get; set; }
        public List<PublicCategoryDto> Categories { get; set; } = new();
    }

    public class PublicCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public List<PublicProductDto> Products { get; set; } = new();
    }

    public class PublicProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public bool ProductIsOnStock { get; set; }
    }
}

