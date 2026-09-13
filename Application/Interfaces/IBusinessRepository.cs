using Mostra.Domain.Entities;

namespace Mostra.Application.Interfaces
{
    public interface IBusinessRepository
    {
        Task<Domain.Entities.Business> CreateAsync(Domain.Entities.Business business, CancellationToken cancellationToken = default);
        Task<List<Domain.Entities.Business>> GetAllAsync(int merchantId, CancellationToken cancellationToken = default);
        Task<Domain.Entities.Business?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Domain.Entities.Business> UpdateAsync(Domain.Entities.Business business, CancellationToken cancellationToken = default);
        Task DeleteAsync(Domain.Entities.Business business, CancellationToken cancellationToken = default);
        Task<Domain.Entities.Business?> GetBySlugWithCatalogAsync(string slug, CancellationToken cancellationToken = default);
    }
}
