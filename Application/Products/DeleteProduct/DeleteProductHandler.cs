using MediatR;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Products.DeleteProduct
{

    public class DeleteProductHandler : IRequestHandler<DeleteProductRequestDto, Unit>
    {
        private readonly IProductRepository _repository;

        public DeleteProductHandler(IProductRepository repository) => _repository = repository;

        public async Task<Unit> Handle(DeleteProductRequestDto request, CancellationToken cancellationToken)
        {
            
            var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (product == null)
                throw new KeyNotFoundException($"Producto con ID {request.Id} no encontrado o ya fue eliminado.");

            
            await _repository.DeleteProductAsync(product, cancellationToken);


            return Unit.Value;
        }
    }

}
