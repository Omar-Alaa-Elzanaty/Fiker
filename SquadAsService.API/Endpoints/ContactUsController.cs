using MediatR;
using Microsoft.AspNetCore.Mvc;
using Fiker.Api.Endpoints;
using Fiker.Application.Features.Contacts.Command.Create;
using Fiker.Domain.Bases;

namespace Fiker.API.Endpoints
{
    public class ContactUsController:ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ContactUsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<int>>>Create([FromBody] CreateContactUsCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
