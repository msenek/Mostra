using Microsoft.EntityFrameworkCore;
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;
using Mostra.Infrastructure.Persistence;

namespace Mostra.Infrastructure.Repository
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly MostraContext _context;

        public MerchantRepository(MostraContext context) => _context = context;

        public async Task<Merchant> CreateAsync(Merchant merchant, CancellationToken cancellationToken = default)
        {
            await _context.Merchants.AddAsync(merchant, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return merchant;
        }

        public async Task<Merchant?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Merchants
                .FirstOrDefaultAsync(m => m.Email == email && !m.IsDeleted, cancellationToken);
        }

        public async Task<Merchant?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Merchants.FindAsync(new object[] { id }, cancellationToken);
        }
    }
}