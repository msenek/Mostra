using MediatR;
using Mostra.Application.Interfaces;
using Mostra.Application.Products.CreateProduct;
using System.ComponentModel.DataAnnotations;

namespace Mostra.Application.Products.UpdateProduct
{
    public class UpdateProductRequestDto : IRequest<UpdateProductResponseDto>, IProductOwnedRequest
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal? ProductPrice { get; set; }
        public bool? ProductIsOnStock { get; set; }
        public bool? IsActive { get; set; }

        public int ProductId => Id;
    }
}
