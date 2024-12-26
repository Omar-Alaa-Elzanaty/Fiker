using Fiker.Application.Interfaces;
using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Bases;
using Fiker.Domain.Domains;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Fiker.Application.Features.Technologies.Queries.GetById
{
    public class GetTechnologyByIdQuery : IRequest<BaseResponse<List<GetTechnologyByIdQueryDto>>>
    {
        public int Id { get; set; }

        public GetTechnologyByIdQuery(int id)
        {
            Id = id;
        }
    }

    internal class GetTechnologyByIdQueryHandler : IRequestHandler<GetTechnologyByIdQuery, BaseResponse<List<GetTechnologyByIdQueryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediaService _mediaService;

        public GetTechnologyByIdQueryHandler(
            IUnitOfWork unitOfWork,
            IMediaService mediaService)
        {
            _unitOfWork = unitOfWork;
            _mediaService = mediaService;
        }

        public async Task<BaseResponse<List<GetTechnologyByIdQueryDto>>> Handle(GetTechnologyByIdQuery request, CancellationToken cancellationToken)
        {
            var technology = await _unitOfWork.Repository<Technology>().Entities
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (technology == null)
            {
                return BaseResponse<List<GetTechnologyByIdQueryDto>>.Fail("Technology not found.", HttpStatusCode.NotFound);
            }

            var jobTitles = await _unitOfWork.Repository<JobTitle>().Entities
                          .Where(x => !technology.JobTitles.Select(x => x.JobTitleId).Contains(x.Id))
                          .ProjectToType<GetTechnologyByIdQueryDto>()
                          .ToListAsync(cancellationToken);

            var technologyJobTitle = technology.JobTitles.Select(x=>x.JobTitle).ToList().Adapt<List<GetTechnologyByIdQueryDto>>();

            technologyJobTitle.ForEach(x => x.IsAvailable = true);

            jobTitles.AddRange(technologyJobTitle);

            return BaseResponse<List<GetTechnologyByIdQueryDto>>.Success(jobTitles);
        }
    }
}
