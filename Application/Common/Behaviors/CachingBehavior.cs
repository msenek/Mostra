using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Mostra.Application.Interfaces;
using System.Text;
using System.Text.Json;

namespace Mostra.Application.Common.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {
        private readonly IDistributedCache _cache;

            public CachingBehavior(IDistributedCache cache)
            {
              _cache = cache;
            }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is not ICacheableRequest cacheableRequest)
            {
                return await next();
            } // return pipeline

            var cachedBytes = await _cache.GetAsync(cacheableRequest.CacheKey, cancellationToken); // cache

            if (cachedBytes != null)
            {
                var cachedJson = Encoding.UTF8.GetString(cachedBytes);
                return JsonSerializer.Deserialize<TResponse>(cachedJson)!; // json pq Idcache son bytes
            } // return cache 

            var response = await next();

            var json = JsonSerializer.Serialize(response); // response = context
            var bytes = Encoding.UTF8.GetBytes(json);// encoding json to Idcache

            var options = new DistributedCacheEntryOptions();
            if (cacheableRequest.Expiration.HasValue)
            {
                options.SetAbsoluteExpiration(cacheableRequest.Expiration.Value);
            }

            await _cache.SetAsync(cacheableRequest.CacheKey, bytes, options, cancellationToken);

            return response;
        }
    }
}
