using MediatR;
using Microsoft.AspNetCore.Mvc;
using Fiker.Api.Endpoints;
using Fiker.Application.Features.Authentication.Login;
using Fiker.Domain.Bases;
using Fiker.Application.Features.Authentication.FogetPassword;
using Fiker.Application.Features.Authentication.ResetPassword;
using Fiker.Application.Features.Authentication.SendConfirmEmailOtp;

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

        [HttpPost("forget-password")]
        public async Task<ActionResult<string>> ForgetPassword(string email)
        {
            return Ok(await _mediator.Send(new ForgetPasswordCommand(email)));
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<string>> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("sendConfirmOtp")]
        public async Task<ActionResult<int>> SendEmailConfirmation([FromBody] string email)
        {
            return Ok(await _mediator.Send(new SendConfirmEmailOtpCommand(email)));
        }
    }
}
