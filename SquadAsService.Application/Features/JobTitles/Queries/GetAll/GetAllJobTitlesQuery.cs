using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Fiker.Application.Interfaces;
using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Bases;
using Fiker.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Features.JobTitles.Queries.GetAll
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
                           .OrderByDescending(x=>x.Name)
                           .ProjectToType<GetAllJobTitlesQueryDto>()
                           .ToListAsync(cancellationToken);

            return BaseResponse<List<GetAllJobTitlesQueryDto>>.Success(jobTitles);
        }
    }
}
