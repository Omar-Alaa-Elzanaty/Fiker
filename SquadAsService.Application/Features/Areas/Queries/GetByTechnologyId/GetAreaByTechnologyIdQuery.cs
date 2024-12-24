using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SquadAsService.Application.Interfaces.Repo;
using SquadAsService.Domain.Bases;
using SquadAsService.Domain.Domains;

namespace SquadAsService.Application.Features.Areas.Queries.GetByTechnologyId
{
    public record GetAreaByTechnologyIdQuery:IRequest<BaseResponse<List<GetAreaByTechnologyIdQueryDto>>>
    {
        public int TechnologyId { get; set; }

        public GetAreaByTechnologyIdQuery(int technologyId)
        {
            TechnologyId = technologyId;
        }
    }

    internal class GetAreaByTechnologyIdQueryHandler : IRequestHandler<GetAreaByTechnologyIdQuery, BaseResponse<List<GetAreaByTechnologyIdQueryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAreaByTechnologyIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<GetAreaByTechnologyIdQueryDto>>> Handle(GetAreaByTechnologyIdQuery command, CancellationToken cancellationToken)
        {
            var areas = await _unitOfWork.Repository<AreaTechonolgy>().Entities
                      .Where(x => x.TechnologyId == command.TechnologyId)
                      .Select(x => x.Area)
                      .ProjectToType<GetAreaByTechnologyIdQueryDto>()
                      .ToListAsync(cancellationToken);

            return BaseResponse<List<GetAreaByTechnologyIdQueryDto>>.Success(areas);
        }
    }
}
