using System.Security.Claims;
using Mostra.Application.Interfaces;

namespace Mostra.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int MerchantId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?.User?
                    .FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(value) || !int.TryParse(value, out var id))
                    throw new UnauthorizedAccessException("The authenticated merchant could not be identified.");

                return id;
            }
        }
    }
}