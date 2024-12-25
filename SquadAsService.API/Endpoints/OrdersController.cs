using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fiker.Application.Features.Orders.Commands.Create;
using Fiker.Application.Features.Orders.Queries.GetAllWithPagination;
using Fiker.Application.Features.Orders.Queries.GetById;
using Fiker.Domain.Bases;
using Fiker.Domain.Constants;

namespace Fiker.Api.Endpoints
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