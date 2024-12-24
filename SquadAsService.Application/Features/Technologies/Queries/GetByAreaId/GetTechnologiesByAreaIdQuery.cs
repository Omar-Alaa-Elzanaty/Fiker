using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SquadAsService.Application.Interfaces;
using SquadAsService.Application.Interfaces.Repo;
using SquadAsService.Domain.Bases;
using SquadAsService.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadAsService.Application.Features.Technologies.Queries.GetByAreaId
{
    public record GetTechnologiesByAreaIdQuery:IRequest<BaseResponse<List<GetTechnologiesByAreaIdQueryDto>>>
    {
        public int AreaId { get; set; }

        public GetTechnologiesByAreaIdQuery(int areaId)
        {
            AreaId = areaId;
        }
    }

    internal class GetTechnologyByAreaIdQueryHandler: IRequestHandler<GetTechnologiesByAreaIdQuery, BaseResponse<List<GetTechnologiesByAreaIdQueryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediaService _mediaService;

        public GetTechnologyByAreaIdQueryHandler(
            IUnitOfWork unitOfWork,
            IMediaService mediaService)
        {
            _unitOfWork = unitOfWork;
            _mediaService = mediaService;
        }

        public async Task<BaseResponse<List<GetTechnologiesByAreaIdQueryDto>>> Handle(GetTechnologiesByAreaIdQuery command, CancellationToken cancellationToken)
        {
            var technologies = await _unitOfWork.Repository<AreaTechonolgy>().Entities
                        .Where(x => x.AreaId == command.AreaId)
                        .Select(x => x.Technology)
                        .ProjectToType<GetTechnologiesByAreaIdQueryDto>()
                        .ToListAsync(cancellationToken);

            technologies.ForEach(x=>x.IconUrl = _mediaService.GetUrl(x.IconUrl));

            return BaseResponse<List<GetTechnologiesByAreaIdQueryDto>>.Success(technologies);
        }
    }
}
