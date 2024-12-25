using MediatR;
using Microsoft.AspNetCore.Mvc;
using Fiker.Application.Features.JobTitles.Commands.Create;
using Fiker.Application.Features.JobTitles.Queries.GetAll;
using Fiker.Application.Features.JobTitles.Queries.GetByTechnologyId;
using Fiker.Domain.Bases;

namespace Fiker.Api.Endpoints
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
