using Microsoft.EntityFrameworkCore;
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;
using Mostra.Infrastructure.Persistence;

namespace Mostra.Infrastructure.Repository
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly MostraContext _context;
        public BusinessRepository(MostraContext context)
        {
            _context = context;
        }

        public async Task<Business> CreateAsync(Business business, CancellationToken cancellationToken)
        {
            await _context.Businesses.AddAsync(business, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return business;
        }

        public async Task<List<Business>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Businesses.ToListAsync<Business>(cancellationToken);
        }

        public async Task<Business?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Businesses.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<Business> UpdateAsync(Business business, CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
            return business;
        }
        public async Task DeleteAsync(Business business, CancellationToken cancellationToken = default)
        {
            
            business.MarkAsDeleted();
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
