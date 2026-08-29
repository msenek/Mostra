using Mostra.Domain.Entities;

namespace Mostra.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category, CancellationToken cancellationToken = default);
        Task<List<Category>> GetAllByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default);
        Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Category> UpdateAsync(Category category, CancellationToken cancellationToken = default);
        Task DeleteAsync(Category category, CancellationToken cancellationToken = default);
    }
}