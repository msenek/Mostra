using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mostra.Application.Categories.CreateCategory;
using Mostra.Application.Categories.DeleteCategory;
using Mostra.Application.Categories.GetCategoriesByBusiness;
using Mostra.Application.Categories.UpdateCategory;
using Mostra.Application.Interfaces;

namespace Mostra.Api.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator, ICategoryRepository repository)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequestDto request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Created("", result);
        }

        [HttpGet("business/{businessId}")]
        public async Task<IActionResult> GetAllByBusiness(int businessId, CancellationToken cancellationToken)
        {
            var request = new GetCategoriesByBusinessRequestDto { BusinessId = businessId };
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryRequestDto request, CancellationToken cancellationToken)
        {
            request.Id = id;
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteCategoryRequestDto { Id = id }, cancellationToken);
            return NoContent();
        }
    }
}