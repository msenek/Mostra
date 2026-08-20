namespace Mostra.Application.Products.GetProduct
{
    public class ListProductsResponseDto
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public bool ProductIsInStock { get; set; }
    }
}
