namespace Mostra.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public int MerchantId { get; set; }

        public string TokenHash { get; set; } = null!;

        public DateTime ExpiresAt { get; set; }

        public DateTime? RevokedAt { get; set; }

        public Merchant Merchant { get; set; } = null!;
    }
}