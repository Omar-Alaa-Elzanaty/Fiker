using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SquadAsService.Api.Endpoints;
using SquadAsService.Application.Features.Areas.Commands.Create;
using SquadAsService.Application.Features.Areas.Commands.Delete;
using SquadAsService.Application.Features.Areas.Queries.GetById;
using SquadAsService.Application.Features.Contacts.Command.Delete;
using SquadAsService.Application.Features.Contacts.Queries.GetAllWithPagination;
using SquadAsService.Application.Features.JobTitles.Commands.Create;
using SquadAsService.Application.Features.Markets.Commands.Create;
using SquadAsService.Application.Features.Markets.Commands.Delete;
using SquadAsService.Application.Features.Orders.Queries.GetAllWithPagination;
using SquadAsService.Application.Features.Orders.Queries.GetById;
using SquadAsService.Application.Features.Technologies.Commands.Create;
using SquadAsService.Application.Features.Technologies.Commands.Delete;
using SquadAsService.Domain.Bases;
using SquadAsService.Domain.Constants;

namespace SquadAsService.API.Endpoints
{
    [Authorize(Roles = Roles.Admin)]
    [Route("api/admin")]
    public class AdminController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("orders")]
        public async Task<ActionResult<BaseResponse<GetAllOrdersWithPaginationQueryDto>>> GetOrders([FromQuery] GetAllOrdersWithPaginationQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("orders/{id}")]
        public async Task<ActionResult<BaseResponse<GetOrderByIdQueryDto>>> GetOrderById([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new GetOrderByIdQuery(id)));
        }

        [HttpPost("areas")]
        public async Task<ActionResult<BaseResponse<int>>> CreateArea([FromBody] CreateAreaCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("areas/{id}")]
        public async Task<ActionResult<BaseResponse<GetAreaByIdQueryDto>>> GetAreaById([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new GetAreaByIdQuery(id)));
        }

        [HttpPost("jobTitles")]
        public async Task<ActionResult<BaseResponse<int>>> CreateJobTitle([FromBody] CreateJobTitlesCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("markets")]
        public async Task<ActionResult<BaseResponse<int>>> CreateMarket([FromBody] CreateMarketCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("technologies")]
        public async Task<ActionResult<BaseResponse<int>>> CreateTechnology([FromBody] CreateTechnologyCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("areas/{id}")]
        public async Task<ActionResult<BaseResponse<string>>> DeleteArea([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new DeleteAreaCommand(id)));
        }

        [HttpGet("contactUs/pagination")]
        public async Task<ActionResult<PaginatedResponse<GetAllContactUsWithPaginationQueryDto>>>
            GetContactUsWithPagination([FromQuery] GetAllContactUsWithPaginationQuery query)
        {
            return Ok(await _mediator.Send(query));
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
    }
}