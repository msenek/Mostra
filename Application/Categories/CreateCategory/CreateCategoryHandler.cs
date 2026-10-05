using MediatR;
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;

namespace Mostra.Application.Categories.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryRequestDto, CreateCategoryResponseDto>
    {
        private readonly ICategoryRepository _repository;
        private readonly ICacheInvalidationService _cacheInvalidation;


        public CreateCategoryHandler(ICategoryRepository repository, ICacheInvalidationService cacheInvalidation)
        {
            _repository = repository;
            _cacheInvalidation = cacheInvalidation;
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

            await _cacheInvalidation.InvalidateCatalogAsync(category.BusinessId, cancellationToken);

            return new CreateCategoryResponseDto
            {
                Id = savedCategory.Id,
                Name = savedCategory.Name,
                BusinessId = savedCategory.BusinessId
            };
        }
    }
}