using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mostra.Application.Businesses.DeleteBusiness;
using Mostra.Application.Businesses.GetAllBusinesses;
using Mostra.Application.Businesses.GetBusinessQrCode;
using Mostra.Application.Businesses.UpdateBusiness;
using Mostra.Application.Bussines.CreateBussines;
using System.Security.Claims;

namespace Mostra.Api.Controllers
{
    [ApiController]
    [Route("api/businesses")] 
    public class BusinessesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BusinessesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBusinessRequestDto request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);

            return Created("", result);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllBusinessesRequestDto(), cancellationToken);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateBusinessRequestDto request, CancellationToken cancellationToken)
        {
            request.Id = id;
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteBusinessRequestDto { Id = id }, cancellationToken);
            return NoContent();
        }

        [Authorize]
        [HttpGet("{id:int}/qrcode")]
        public async Task<IActionResult> GetQrCode(int id, CancellationToken cancellationToken)
        {
            var pngBytes = await _mediator.Send(new GetBusinessQrCodeRequestDto { BusinessId = id }, cancellationToken);
            return File(pngBytes, "image/png");
        }
    }
}