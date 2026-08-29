using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Mostra.Application.Products.CreateProduct
{
    public class CreateProductRequestDto : IRequest<CreateProductResponseDto>
    {

        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public bool ProductIsOnStock { get; set; }
        public int BusinessId { get; set; }
        public int CategoryId { get; set; }
    }
}
