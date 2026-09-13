using MediatR;
using Mostra.Application.Interfaces;
using Mostra.Application.Products.UploadImageToProduct;

public class UploadProductImageRequestDto : IRequest<UploadProductImageResponseDto>, IProductOwnedRequest
{
    public int Id { get; set; }
    public IFormFile File { get; set; } = null!;

    public int ProductId => Id;
}