using MediatR;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Products.DeleteProduct
{

    public class DeleteProductHandler : IRequestHandler<DeleteProductRequestDto, Unit>
    {
        private readonly IProductRepository _repository;
        private readonly ICacheInvalidationService _cacheInvalidation;

        public DeleteProductHandler(IProductRepository repository, ICacheInvalidationService cacheInvalidation)
        {
            _repository = repository;
            _cacheInvalidation = cacheInvalidation;
        }

        public async Task<Unit> Handle(DeleteProductRequestDto request, CancellationToken cancellationToken)
        {
            
            var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (product == null)
                throw new KeyNotFoundException($"Producto con ID {request.Id} no encontrado o ya fue eliminado.");

            
            await _repository.DeleteProductAsync(product, cancellationToken);

            await _cacheInvalidation.InvalidateCatalogAsync(product.BusinessId, cancellationToken);

            return Unit.Value;
        }
    }

}
