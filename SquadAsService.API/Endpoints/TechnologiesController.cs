using MediatR;
using Microsoft.AspNetCore.Mvc;
using Fiker.Application.Features.Technologies.Commands.Create;
using Fiker.Application.Features.Technologies.Queries.GetAll;
using Fiker.Application.Features.Technologies.Queries.GetByAreaId;
using Fiker.Application.Features.Technologies.Queries.GetById;
using Fiker.Domain.Bases;

namespace Fiker.Api.Endpoints
{
    public class TechnologiesController: ApiControllerBase
    {
        private readonly IMediator _mediator;

        public TechnologiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<List<GetAllTechnologiesQueryDto>>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllTechnologiesQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<GetTechnologyByIdQueryDto>>>GetById([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new GetTechnologyByIdQuery(id)));
        }

        [HttpGet("area/{areaId}")]
        public async Task<ActionResult<BaseResponse<GetTechnologiesByAreaIdQueryDto>>> GetByAreaId([FromRoute] int areaId)
        {
            return Ok(await _mediator.Send(new GetTechnologiesByAreaIdQuery(areaId)));
        }
    }
}
