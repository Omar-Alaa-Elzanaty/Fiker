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

namespace SquadAsService.Application.Features.JobTitles.Queries.GetAll
{
    public record GetAllJobTitlesQuery:IRequest<BaseResponse<List<GetAllJobTitlesQueryDto>>>;

    internal class GetAllJobTitlesQueryHandler : IRequestHandler<GetAllJobTitlesQuery, BaseResponse<List<GetAllJobTitlesQueryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllJobTitlesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<GetAllJobTitlesQueryDto>>> Handle(GetAllJobTitlesQuery request, CancellationToken cancellationToken)
        {
            var jobTitles = await _unitOfWork.Repository<JobTitle>().Entities
                           .ProjectToType<GetAllJobTitlesQueryDto>()
                           .ToListAsync(cancellationToken);

            return BaseResponse<List<GetAllJobTitlesQueryDto>>.Success(jobTitles);
        }
    }
}
