using MediatR;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Categories.GetCategoriesByBusiness
{
    public class GetCategoriesByBusinessHandler : IRequestHandler<GetCategoriesByBusinessRequestDto, List<GetCategoriesByBusinessResponseDto>>
    {
        private readonly ICategoryRepository _repository;

        public GetCategoriesByBusinessHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetCategoriesByBusinessResponseDto>> Handle(GetCategoriesByBusinessRequestDto request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAllByBusinessIdAsync(request.BusinessId, cancellationToken);

            return categories.Select(c => new GetCategoriesByBusinessResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();
        }
    }
}