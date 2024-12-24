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

namespace SquadAsService.Application.Features.Areas.Queries.GetAll
{
    public record GetAllAreasQuery:IRequest<BaseResponse<List<GetAllAreasQueryDto>>>
    {
        
    }

    internal class GetAllAreasQueryHandler : IRequestHandler<GetAllAreasQuery, BaseResponse<List<GetAllAreasQueryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllAreasQueryHandler(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<GetAllAreasQueryDto>>> Handle(GetAllAreasQuery request, CancellationToken cancellationToken)
        {
            var areas = await _unitOfWork.Repository<Area>().Entities
                        .ProjectToType<GetAllAreasQueryDto>()
                        .ToListAsync(cancellationToken);

            return BaseResponse<List<GetAllAreasQueryDto>>.Success(areas);
        }
    }
}
