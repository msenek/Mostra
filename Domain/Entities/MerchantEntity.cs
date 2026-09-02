namespace Mostra.Domain.Entities
{
    public class Merchant
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Business> Businesses { get; set; } = new List<Business>();

        public void MarkAsDeleted() => IsDeleted = true;
    }
}