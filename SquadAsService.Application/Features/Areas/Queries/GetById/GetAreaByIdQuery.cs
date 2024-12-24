using Mapster;
using MediatR;
using SquadAsService.Application.Interfaces;
using SquadAsService.Application.Interfaces.Repo;
using SquadAsService.Domain.Bases;
using SquadAsService.Domain.Domains;
using System.Net;

namespace SquadAsService.Application.Features.Areas.Queries.GetById
{
    public record GetAreaByIdQuery : IRequest<BaseResponse<GetAreaByIdQueryDto>>
    {
        public int Id { get; set; }

        public GetAreaByIdQuery(int id)
        {
            Id = id;
        }
    }

    internal class GetAreaByIdQueryHandler : IRequestHandler<GetAreaByIdQuery, BaseResponse<GetAreaByIdQueryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediaService _mediaService;

        public GetAreaByIdQueryHandler(
            IUnitOfWork unitOfWork,
            IMediaService mediaService)
        {
            _unitOfWork = unitOfWork;
            _mediaService = mediaService;
        }

        public async Task<BaseResponse<GetAreaByIdQueryDto>> Handle(GetAreaByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<Area>().GetByIdAsync(request.Id);

            if (entity == null)
            {
                return BaseResponse<GetAreaByIdQueryDto>.Fail("Area is not found.", HttpStatusCode.NotFound);
            }

            var area = entity.Adapt<GetAreaByIdQueryDto>();

            area.Techonolgies.ForEach(x => x.IconUrl = _mediaService.GetUrl(x.IconUrl));

            return BaseResponse<GetAreaByIdQueryDto>.Success(area);
        }
    }
}
