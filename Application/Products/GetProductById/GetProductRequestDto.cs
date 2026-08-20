using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Mostra.Application.Products.GetProductById
{
    public class GetProductRequestDto : IRequest<GetProductResponseDto>
    {
        public int Id { get; set; }
    }
}
