using System.ComponentModel.DataAnnotations;

namespace Mostra.Application.Products.UpdateProduct
{
    public class UpdateProductResponseDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
       
        public string ProductDescription { get; set; }
        
        public decimal ProductPrice { get; set; }
        
        public bool ProductIsOnStock { get; set; }
    }
}
