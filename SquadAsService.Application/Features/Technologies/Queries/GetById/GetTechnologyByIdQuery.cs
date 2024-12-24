using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SquadAsService.Application.Interfaces;
using SquadAsService.Application.Interfaces.Repo;
using SquadAsService.Domain.Bases;
using SquadAsService.Domain.Domains;
using System.Net;

namespace SquadAsService.Application.Features.Technologies.Queries.GetById
{
    public class GetTechnologyByIdQuery : IRequest<BaseResponse<GetTechnologyByIdQueryDto>>
    {
        public int Id { get; set; }

        public GetTechnologyByIdQuery(int id)
        {
            Id = id;
        }
    }

    internal class GetTechnologyByIdQueryHandler : IRequestHandler<GetTechnologyByIdQuery, BaseResponse<GetTechnologyByIdQueryDto>>
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

        public async Task<BaseResponse<GetTechnologyByIdQueryDto>> Handle(GetTechnologyByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<Technology>().Entities
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                return BaseResponse<GetTechnologyByIdQueryDto>.Fail("Technology not found.", HttpStatusCode.NotFound);
            }

            var techonology = entity.Adapt<GetTechnologyByIdQueryDto>();

            techonology.IconUrl = _mediaService.GetUrl(entity.IconUrl)!;

            return BaseResponse<GetTechnologyByIdQueryDto>.Success(techonology);
        }
    }
}
