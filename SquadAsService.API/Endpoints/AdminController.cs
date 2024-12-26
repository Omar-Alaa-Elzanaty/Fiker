using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fiker.Api.Endpoints;
using Fiker.Application.Features.Areas.Commands.Create;
using Fiker.Application.Features.Areas.Commands.Delete;
using Fiker.Application.Features.Areas.Queries.GetById;
using Fiker.Application.Features.Contacts.Command.Delete;
using Fiker.Application.Features.Contacts.Queries.GetAllWithPagination;
using Fiker.Application.Features.JobTitles.Commands.Create;
using Fiker.Application.Features.Markets.Commands.Create;
using Fiker.Application.Features.Markets.Commands.Delete;
using Fiker.Application.Features.Orders.Commands.Delete;
using Fiker.Application.Features.Orders.Queries.GetAllWithPagination;
using Fiker.Application.Features.Orders.Queries.GetById;
using Fiker.Application.Features.Technologies.Commands.Create;
using Fiker.Application.Features.Technologies.Commands.Delete;
using Fiker.Domain.Bases;
using Fiker.Domain.Constants;
using Fiker.Application.Features.Admin.Commands.Create;
using Fiker.Application.Features.Technologies.Queries.GetById;
using Fiker.Application.Features.Admin.Queries.GetAll;

namespace Fiker.API.Endpoints
{
    [Authorize(Roles = $"{Roles.Admin},{Roles.SuperAdmin}")]
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
        public async Task<ActionResult<BaseResponse<List<GetAreaByIdQueryDto>>>> GetAreaById([FromRoute] int id)
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

        [HttpGet("technologies/{id}")]
        public async Task<ActionResult<BaseResponse<List<GetTechnologyByIdQueryDto>>>> GetById([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new GetTechnologyByIdQuery(id)));
        }

        [HttpPost("technologies")]
        public async Task<ActionResult<BaseResponse<int>>> CreateTechnology([FromBody] CreateTechnologyCommand command)
        {
            return Ok(await _mediator.Send(command));
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

        [HttpDelete("orders/{id}")]
        public async Task<ActionResult<BaseResponse<string>>> DeleteOrder([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new DeleteOrderCommand(id)));
        }

        [HttpGet("admins")]
        public async Task<ActionResult<BaseResponse<List<GetAllAdminsQueryDto>>>> GetAllAdmins()
        {
            return Ok(await _mediator.Send(new GetAllAdminsQuery()));
        }
    }
}