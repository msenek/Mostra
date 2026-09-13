using Microsoft.EntityFrameworkCore;
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;
using Mostra.Infrastructure.Persistence;

namespace Mostra.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MostraContext _context;
        public ProductRepository(MostraContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync();
            return product;
        }
         
        public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {

            return await _context.Products.ToListAsync(cancellationToken);
        }

        public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default, bool includeDeleted = false)
        {
            var query = _context.Products.AsQueryable();

            if (includeDeleted)
            {
                query = query.IgnoreQueryFilters();
            }
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> UpdateProductAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }

        public async Task DeleteProductAsync(Product product, CancellationToken cancellationToken)
        {
            product.MarkAsDeleted();
            await _context.SaveChangesAsync(cancellationToken);

            
        }

        public async Task<List<Product>> GetAllByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default)
        {
            
            return await _context.Products
                .Where(p => p.BusinessId == businessId)
                .ToListAsync(cancellationToken);
        }


    }
}
