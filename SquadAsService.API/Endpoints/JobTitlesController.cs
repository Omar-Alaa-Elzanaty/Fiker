using MediatR;
using Microsoft.AspNetCore.Mvc;
using SquadAsService.Application.Features.JobTitles.Commands.Create;
using SquadAsService.Application.Features.JobTitles.Queries.GetAll;
using SquadAsService.Application.Features.JobTitles.Queries.GetByTechnologyId;
using SquadAsService.Domain.Bases;

namespace SquadAsService.Api.Endpoints
{
    public class JobTitlesController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public JobTitlesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<List<GetAllJobTitlesQueryDto>>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllJobTitlesQuery()));
        }

        [HttpGet("technology/{technologyId}")]
        public async Task<ActionResult<BaseResponse<List<GetJobTitleByTechnologyIdQueryDto>>>> GetJobTitlesByTechnologyId([FromRoute] int technologyId)
        {
            return Ok(await _mediator.Send(new GetJobTitleByTechnologyIdQuery(technologyId)));
        }
    }
}
