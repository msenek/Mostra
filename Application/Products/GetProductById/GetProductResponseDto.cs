using System.ComponentModel.DataAnnotations;

namespace Mostra.Application.Products.GetProductById
{
    public class GetProductResponseDto
    {
        public int Id { get; set; }

        public string? ProductName { get; set; }

        public string? ProductDescription { get; set; }

        public decimal ProductPrice { get; set; }

        public bool ProductIsOnStock { get; set; }
    }
}
