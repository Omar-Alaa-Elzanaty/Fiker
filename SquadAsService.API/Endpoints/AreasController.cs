using MediatR;
using Microsoft.AspNetCore.Mvc;
using Fiker.Application.Features.Areas.Commands.Create;
using Fiker.Application.Features.Areas.Queries.GetAll;
using Fiker.Application.Features.Areas.Queries.GetByTechnologyId;
using Fiker.Domain.Bases;

namespace Fiker.Api.Endpoints
{
    public class AreasController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AreasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<List<GetAllAreasQueryDto>>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllAreasQuery()));
        }

        [HttpGet("technology/{technologyId}")]
        public async Task<ActionResult<BaseResponse<List<GetAreaByTechnologyIdQueryDto>>>> GetByTechnologyId([FromRoute] int technologyId)
        {
            return Ok(await _mediator.Send(new GetAreaByTechnologyIdQuery(technologyId)));
        }
    }
}
