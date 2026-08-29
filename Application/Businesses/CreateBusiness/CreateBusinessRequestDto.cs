using MediatR;

namespace Mostra.Application.Bussines.CreateBussines
{
    public class CreateBusinessRequestDto : IRequest<CreateBusinessResponseDto>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? LogoUrl { get; set; }
    }
}
