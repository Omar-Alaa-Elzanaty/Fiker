using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Bases;
using Fiker.Domain.Domains;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fiker.Application.Features.Areas.Queries.GetAll
{
    public record GetAllAreasQuery : IRequest<BaseResponse<List<GetAllAreasQueryDto>>>
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
                        .OrderBy(x => x.Name)
                        .ProjectToType<GetAllAreasQueryDto>()
                        .ToListAsync(cancellationToken);

            return BaseResponse<List<GetAllAreasQueryDto>>.Success(areas);
        }
    }
}
