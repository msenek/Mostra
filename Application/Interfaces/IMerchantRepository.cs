using Mostra.Domain.Entities;

namespace Mostra.Application.Interfaces
{
    public interface IMerchantRepository
    {
        Task<Merchant> CreateAsync(Merchant merchant, CancellationToken cancellationToken = default);
        Task<Merchant?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<Merchant?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}