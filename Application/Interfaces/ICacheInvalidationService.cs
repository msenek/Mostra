namespace Mostra.Application.Interfaces
{
    public interface ICacheInvalidationService
    {
        Task InvalidateCatalogAsync(int businessId, CancellationToken cancellationToken = default);
    }
}