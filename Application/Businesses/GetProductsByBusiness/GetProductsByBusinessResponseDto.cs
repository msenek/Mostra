using MediatR;
using Mostra.Domain.Entities;

namespace Mostra.Application.Products.GetProductsByBusiness
{
    public class GetProductsByBusinessResponseDto
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public bool ProductIsOnStock { get; set; }
        public int BusinessId { get; set; }
        public int CategoryId { get; set; }
    }
}