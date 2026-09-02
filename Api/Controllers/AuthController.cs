using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mostra.Application.Auth.Register;
using Mostra.Application.Auth.Login;

namespace Mostra.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator) => _mediator = mediator;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Created("", result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}