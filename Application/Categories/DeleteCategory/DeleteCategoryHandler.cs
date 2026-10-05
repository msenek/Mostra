using MediatR;
using Mostra.Application.Exceptions;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Categories.DeleteCategory
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryRequestDto, Unit>
    {
        private readonly ICategoryRepository _repository;
        private readonly ICacheInvalidationService _cacheInvalidation;



        public DeleteCategoryHandler(ICategoryRepository repository, ICacheInvalidationService cacheInvalidation)
        {
            _repository = repository;
            _cacheInvalidation = cacheInvalidation;

        }


        public async Task<Unit> Handle(DeleteCategoryRequestDto request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException("Category not found.");

            category.MarkAsDeleted();
            await _repository.DeleteAsync(category, cancellationToken);

            await _cacheInvalidation.InvalidateCatalogAsync(category.BusinessId, cancellationToken);

            return Unit.Value;
        }
    }
}