using System.ComponentModel.DataAnnotations;
namespace Mostra.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public bool ProductIsOnStock { get; set; }
        public bool IsDeleted { get; set; }

        public int BusinessId { get; set; }
        public Business? Business { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public void MarkAsDeleted()
        {
            IsDeleted = true;
        }


    }
}
