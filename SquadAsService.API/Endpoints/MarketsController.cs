using MediatR;
using Microsoft.AspNetCore.Mvc;
using SquadAsService.Application.Features.Markets.Commands.Create;
using SquadAsService.Application.Features.Markets.Queries.GetAll;
using SquadAsService.Domain.Bases;

namespace SquadAsService.Api.Endpoints
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
