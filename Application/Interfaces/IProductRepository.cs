using Mostra.Domain.Entities;

namespace Mostra.Application.Interfaces
{
    public interface IProductRepository
    {
        public Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken = default);

        public Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);

        public Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default, bool includeDeleted = false);

        public Task<Product> UpdateProductAsync(Product product, CancellationToken cancellationToken = default);

        public Task DeleteProductAsync(Product product, CancellationToken cancellationToken = default);

        Task<List<Product>> GetAllByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default);
    }
    
}
