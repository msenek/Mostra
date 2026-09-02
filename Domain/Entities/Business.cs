namespace Mostra.Domain.Entities
{
    public class Business
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? LogoUrl { get; set; } 
        public string UniqueSlug { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public int MerchantId { get; set; }
        public Merchant Merchant { get; set; } = null!;
        public void MarkAsDeleted()
        {
            IsDeleted = true;
        }

    }
}
