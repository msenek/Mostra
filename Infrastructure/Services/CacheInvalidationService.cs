using Microsoft.Extensions.Caching.Distributed;
using Mostra.Application.Interfaces;

namespace Mostra.Infrastructure.Services
{
    public class CacheInvalidationService : ICacheInvalidationService
    {
        private readonly IDistributedCache _cache;
        private readonly IBusinessRepository _businessRepository;

        public CacheInvalidationService(IDistributedCache cache, IBusinessRepository businessRepository)
        {
            _cache = cache;
            _businessRepository = businessRepository;
        }

        public async Task InvalidateCatalogAsync(int businessId, CancellationToken cancellationToken = default)
        {
            var business = await _businessRepository.GetByIdAsync(businessId, cancellationToken);
            if (business == null)
                return;

            await _cache.RemoveAsync($"catalog:{business.UniqueSlug}", cancellationToken);
        }
    }
}