using MediatR;
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;

namespace Mostra.Application.Categories.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryRequestDto, CreateCategoryResponseDto>
    {
        private readonly ICategoryRepository _repository;

        public CreateCategoryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateCategoryResponseDto> Handle(CreateCategoryRequestDto request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
                BusinessId = request.BusinessId
            };

            var savedCategory = await _repository.CreateAsync(category, cancellationToken);

            return new CreateCategoryResponseDto
            {
                Id = savedCategory.Id,
                Name = savedCategory.Name,
                BusinessId = savedCategory.BusinessId
            };
        }
    }
}