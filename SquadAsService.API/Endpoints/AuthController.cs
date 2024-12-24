using MediatR;
using Microsoft.AspNetCore.Mvc;
using SquadAsService.Api.Endpoints;
using SquadAsService.Application.Features.Authentication.Login;
using SquadAsService.Domain.Bases;

namespace SquadAsService.API.Endpoints
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
