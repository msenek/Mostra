using Microsoft.EntityFrameworkCore;
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;
using Mostra.Infrastructure.Persistence;

namespace Mostra.Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MostraContext _context;

        public CategoryRepository(MostraContext context) => _context = context;

        public async Task<Category> CreateAsync(Category category, CancellationToken cancellationToken = default)
        {
            await _context.Categories.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return category;
        }

        public async Task<List<Category>> GetAllByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .Where(c => c.BusinessId == businessId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<Category> UpdateAsync(Category category, CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
            return category;
        }
        public async Task DeleteAsync(Category category, CancellationToken cancellationToken = default)
        {
            category.MarkAsDeleted();
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}