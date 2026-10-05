namespace Mostra.Application.Interfaces
{
    public interface ICacheableRequest
    {
        string CacheKey { get; }
        TimeSpan? Expiration { get; }
    }
}
