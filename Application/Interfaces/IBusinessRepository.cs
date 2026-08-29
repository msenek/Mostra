using Mostra.Domain.Entities;

namespace Mostra.Application.Interfaces
{
    public interface IBusinessRepository
    {
        Task<Business> CreateAsync(Business business, CancellationToken cancellationToken = default);
        Task<List<Business>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Business?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Business> UpdateAsync(Business business, CancellationToken cancellationToken = default);
        Task DeleteAsync(Business business, CancellationToken cancellationToken = default);
    }
}
