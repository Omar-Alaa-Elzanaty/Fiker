using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Bases;
using Fiker.Domain.Domains;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fiker.Application.Features.JobTitles.Queries.GetByTechnologyId
{
    public record GetJobTitleByTechnologyIdQuery : IRequest<BaseResponse<List<GetJobTitleByTechnologyIdQueryDto>>>
    {
        public int TechnologyId { get; set; }

        public GetJobTitleByTechnologyIdQuery(int technologyId)
        {
            TechnologyId = technologyId;
        }
    }

    internal class GetJobTitleByTechnologyIdQueryHandler : IRequestHandler<GetJobTitleByTechnologyIdQuery, BaseResponse<List<GetJobTitleByTechnologyIdQueryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetJobTitleByTechnologyIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<GetJobTitleByTechnologyIdQueryDto>>> Handle(GetJobTitleByTechnologyIdQuery request, CancellationToken cancellationToken)
        {
            var jobTitles = await _unitOfWork.Repository<TechnologyJobTitle>().Entities
                .Where(x => x.TechnologyId == request.TechnologyId)
                .Select(x => x.JobTitle)
                .OrderBy(x => x.Name)
                .ProjectToType<GetJobTitleByTechnologyIdQueryDto>()
                .ToListAsync(cancellationToken);

            return BaseResponse<List<GetJobTitleByTechnologyIdQueryDto>>.Success(jobTitles);
        }
    }
}
