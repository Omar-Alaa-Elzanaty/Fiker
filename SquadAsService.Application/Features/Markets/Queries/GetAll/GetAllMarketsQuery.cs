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

namespace Fiker.Application.Features.Markets.Queries.GetAll
{
    public record GetAllMarketsQuery:IRequest<BaseResponse<List<GetAllMarketsQueryQueryDto>>>;

    internal class GetAllMarketsQueryHandler : IRequestHandler<GetAllMarketsQuery, BaseResponse<List<GetAllMarketsQueryQueryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediaService _mediaService;

        public GetAllMarketsQueryHandler(IUnitOfWork unitOfWork,
            IMediaService mediaService)
        {
            _unitOfWork = unitOfWork;
            _mediaService = mediaService;
        }

        public async Task<BaseResponse<List<GetAllMarketsQueryQueryDto>>> Handle(GetAllMarketsQuery request, CancellationToken cancellationToken)
        {
            var markets = await _unitOfWork.Repository<Market>().Entities
                        .ProjectToType<GetAllMarketsQueryQueryDto>()
                        .ToListAsync(cancellationToken);

            markets.ForEach(markets => markets.IconUrl = _mediaService.GetUrl(markets.IconUrl)!);

            return BaseResponse<List<GetAllMarketsQueryQueryDto>>.Success(markets);
        }
    }
}
