using Fiker.Application.Interfaces;
using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Bases;
using Fiker.Domain.Domains;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Fiker.Application.Features.Areas.Queries.GetById
{
    public record GetAreaByIdQuery : IRequest<BaseResponse<List<GetAreaByIdQueryDto>>>
    {
        public int Id { get; set; }

        public GetAreaByIdQuery(int id)
        {
            Id = id;
        }
    }

    internal class GetAreaByIdQueryHandler : IRequestHandler<GetAreaByIdQuery, BaseResponse<List<GetAreaByIdQueryDto>>>
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

        public async Task<BaseResponse<List<GetAreaByIdQueryDto>>> Handle(GetAreaByIdQuery query, CancellationToken cancellationToken)
        {
            var area = await _unitOfWork.Repository<Area>().Entities
                        .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

            if (area == null)
            {
                return BaseResponse<List<GetAreaByIdQueryDto>>.Fail("Area is not found.", HttpStatusCode.NotFound);
            }

            var technologies = await _unitOfWork.Repository<Technology>().Entities
                            .Where(x => !area.Techonolgies.Select(x => x.TechnologyId).Contains(x.Id))
                            .ProjectToType<GetAreaByIdQueryDto>()
                            .ToListAsync(cancellationToken);

            var areaTechnology = area.Techonolgies.Select(x=>x.Technology).ToList().Adapt<List<GetAreaByIdQueryDto>>();
            areaTechnology.ForEach(x => x.IsAvailable = true);

            technologies.AddRange(areaTechnology);

            technologies.ForEach(x => x.IconUrl = _mediaService.GetUrl(x.IconUrl));

            return BaseResponse<List<GetAreaByIdQueryDto>>.Success(technologies);
        }
    }
}
