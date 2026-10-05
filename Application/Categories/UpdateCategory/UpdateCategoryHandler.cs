using MediatR;
using Mostra.Application.Exceptions;
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;

namespace Mostra.Application.Categories.UpdateCategory
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryRequestDto, UpdateCategoryResponseDto>
    {
        private readonly ICategoryRepository _repository;
        private readonly ICacheInvalidationService _cacheInvalidation;

        public UpdateCategoryHandler(ICategoryRepository repository, ICacheInvalidationService cacheInvalidation)
        {
            _repository = repository;
            _cacheInvalidation = cacheInvalidation;

        }

        public async Task<UpdateCategoryResponseDto> Handle(UpdateCategoryRequestDto request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException("Category not found");

            if (!string.IsNullOrWhiteSpace(request.Name)) category.Name = request.Name;
            if (request.Description != null) category.Description = request.Description;

            await _repository.UpdateAsync(category, cancellationToken);

            await _cacheInvalidation.InvalidateCatalogAsync(category.BusinessId, cancellationToken);


            return new UpdateCategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                BusinessId = category.BusinessId
            };
        }
    }
}