using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SquadAsService.Application.Features.Orders.Commands.Create;
using SquadAsService.Application.Features.Orders.Queries.GetAllWithPagination;
using SquadAsService.Application.Features.Orders.Queries.GetById;
using SquadAsService.Domain.Bases;
using SquadAsService.Domain.Constants;

namespace SquadAsService.Api.Endpoints
{
    public class OrdersController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<int>>> Create([FromBody] CreateOrderCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}