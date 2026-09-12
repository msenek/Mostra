using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mostra.Application.Products.CreateProduct;
using Mostra.Application.Products.DeleteProduct;
using Mostra.Application.Products.GetProduct;
using Mostra.Application.Products.GetProductById;
using Mostra.Application.Products.GetProductsByBusiness;
using Mostra.Application.Products.UpdateProduct;
using Mostra.Application.Products.UpdateProductIsActive;

namespace Mostra.Api.Controllers

{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequestDto request)
        {
            var result = await _mediator.Send(request);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ListProductsRequestDto(), cancellationToken);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var request = new GetProductRequestDto { Id = id };

            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        [Authorize]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateProductRequestDto requestDto, CancellationToken cancellationToken)
        {
            requestDto.Id = id;
            var result = await _mediator.Send(requestDto, cancellationToken);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> UpdateIsActive(int Id, UpdateProductIsActiveRequestDto requestDto, CancellationToken cancellationToken)
        {
            requestDto.Id = Id;
            var result = await _mediator.Send(requestDto, cancellationToken);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var request = new DeleteProductRequestDto { Id = id };
            await _mediator.Send(request, cancellationToken);

            return NoContent();
        }
        [Authorize]
        [HttpGet("business/{businessId:int}")]
        public async Task<IActionResult> GetAllByBusiness(int businessId, CancellationToken cancellationToken)
        {
            var request = new GetProductsByBusinessRequestDto { BusinessId = businessId };
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
