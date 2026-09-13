using MediatR;
using Microsoft.AspNetCore.Http;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Products.UpdateProductImage
{
    public class UpdateProductImageRequestDto : IRequest<UpdateProductImageResponseDto>, IProductOwnedRequest
    {
        public int Id { get; set; }
        public IFormFile File { get; set; } = null!;
        public int ProductId => Id;
    }
}