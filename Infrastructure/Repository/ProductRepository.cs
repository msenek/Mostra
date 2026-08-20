using Microsoft.EntityFrameworkCore;
using Mostra.Application.Interfaces;
using Mostra.Infraestructure.Persistence;
using Mostra.Domain.Entities;

namespace Mostra.Infraestructure.Repository
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

            return await _context.Products.Where(p => !p.IsDeleted).ToListAsync(cancellationToken);
        }

        public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken);
        }

        public async Task<Product> UpdateProductAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }

        public async Task DeleteProductAsync(Product product, CancellationToken cancellationToken)
        {
            product.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            
        }


    }
}
