using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Mostra.Application.Products.CreateProduct
{
    public class CreateProductRequestDto : IRequest<CreateProductResponseDto>
    {
        [Required]
        [MinLength(5)]
        public string? ProductName { get; set; }
        [Required]
        [MinLength(50)]
        [MaxLength(800)]
        public string? ProductDescription { get; set; }
        [Required]
        public decimal ProductPrice { get; set; }
        [Required]
        public bool ProductIsOnStock { get; set; }
    }
}
