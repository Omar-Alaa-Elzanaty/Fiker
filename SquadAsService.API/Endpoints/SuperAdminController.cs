using Fiker.Api.Endpoints;
using Fiker.Application.Features.Admin.Commands.Create;
using Fiker.Application.Features.Admin.Commands.Delete;
using Fiker.Application.Features.Areas.Commands.Delete;
using Fiker.Application.Features.Contacts.Command.Delete;
using Fiker.Application.Features.JobTitles.Commands.EditFromTechnologyId;
using Fiker.Application.Features.Markets.Commands.Delete;
using Fiker.Application.Features.Orders.Commands.Delete;
using Fiker.Application.Features.Technologies.Commands.Delete;
using Fiker.Application.Features.Technologies.Commands.RemoveFromAreaId;
using Fiker.Domain.Bases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fiker.API.Endpoints
{
    public class SuperAdminController:ApiControllerBase
    {
        private readonly IMediator _mediator;

        public SuperAdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("admin")]
        public async Task<ActionResult<BaseResponse<string>>> CreateUser([FromBody] CreateAdminCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("admin/{id}")]
        public async Task<ActionResult<BaseResponse<string>>> DeleteUser([FromRoute] string id)
        {
            return Ok(await _mediator.Send(new DeleteAdminCommand(id)));
        }

        [HttpDelete("areas/{id}")]
        public async Task<ActionResult<BaseResponse<string>>> DeleteArea([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new DeleteAreaCommand(id)));
        }

        [HttpDelete("contactUs/{id}")]
        public async Task<ActionResult<BaseResponse<DeleteContactUsCommand>>> DeleteContactUs([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new DeleteContactUsCommand(id)));
        }

        [HttpDelete("markets/{id}")]
        public async Task<ActionResult<BaseResponse<string>>> DeleteMarket([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new DeleteMarketCommand(id)));
        }

        [HttpDelete("technologies/{id}")]
        public async Task<ActionResult<BaseResponse<string>>> DeleteTechnology([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new DeleteTechnologyCommand(id)));
        }

        [HttpDelete("orders/{id}")]
        public async Task<ActionResult<BaseResponse<string>>> DeleteOrder([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new DeleteOrderCommand(id)));
        }

        [HttpPut("technogolies/profile")]
        public async Task<ActionResult<BaseResponse<string>>> EditTechnologyProfile([FromBody] EditFromTechnologyIdCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("areas/technology")]
        public async Task<ActionResult<BaseResponse<string>>> EditAreaTechnology([FromBody] EditFromAreaIdCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
