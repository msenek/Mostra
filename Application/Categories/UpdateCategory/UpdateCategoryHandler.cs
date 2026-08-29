using MediatR;
using Mostra.Application.Exceptions;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Categories.UpdateCategory
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryRequestDto, UpdateCategoryResponseDto>
    {
        private readonly ICategoryRepository _repository;

        public UpdateCategoryHandler(ICategoryRepository repository) => _repository = repository;

        public async Task<UpdateCategoryResponseDto> Handle(UpdateCategoryRequestDto request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException("Category not found");

            if (!string.IsNullOrWhiteSpace(request.Name)) category.Name = request.Name;
            if (request.Description != null) category.Description = request.Description;

            await _repository.UpdateAsync(category, cancellationToken);

            return new UpdateCategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                BusinessId = category.BusinessId
            };
        }
    }
}