using MediatR;
using Microsoft.AspNetCore.Mvc;
using SquadAsService.Api.Endpoints;
using SquadAsService.Application.Features.Contacts.Command.Create;
using SquadAsService.Domain.Bases;

namespace SquadAsService.API.Endpoints
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
