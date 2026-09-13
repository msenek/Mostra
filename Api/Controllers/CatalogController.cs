using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Mostra.Application.Catalog.GetPublicCatalog;

namespace Mostra.Api.Controllers
{
    [ApiController]
    [Route("api/catalog")]
    public class CatalogController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CatalogController(IMediator mediator) => _mediator = mediator;

        [EnableRateLimiting("qr-reader")]
        [HttpGet("{slug}")] 
        public async Task<IActionResult> GetBySlug(string slug, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPublicCatalogRequestDto { Slug = slug }, cancellationToken);
            return Ok(result);
        }
    }
}