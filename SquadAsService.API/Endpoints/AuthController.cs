using MediatR;
using Microsoft.AspNetCore.Mvc;
using Fiker.Api.Endpoints;
using Fiker.Application.Features.Authentication.Login;
using Fiker.Domain.Bases;

namespace Fiker.API.Endpoints
{
    public class AuthController:ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<LoginQueryDto>>> Login([FromBody] LoginQuery command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
