using MediatR;
using Microsoft.AspNetCore.Mvc;
using Fiker.Application.Features.Markets.Commands.Create;
using Fiker.Application.Features.Markets.Queries.GetAll;
using Fiker.Domain.Bases;

namespace Fiker.Api.Endpoints
{
    public class MarketsController:ApiControllerBase
    {
        private readonly IMediator _mediator;
        public MarketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<List<GetAllMarketsQueryQueryDto>>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllMarketsQuery()));
        }
    }
}
