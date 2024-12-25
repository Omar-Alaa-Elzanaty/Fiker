using MediatR;
using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Bases;
using Fiker.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Fiker.Application.Interfaces;

namespace Fiker.Application.Features.Markets.Commands.Delete
{
    public record DeleteMarketCommand:IRequest<BaseResponse<string>>
    {
        public int Id { get; set; }

        public DeleteMarketCommand(int id)
        {
            Id = id;
        }
    }

    internal class DeleteMarketCommandHandler:IRequestHandler<DeleteMarketCommand, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediaService _mediaService;

        public DeleteMarketCommandHandler(
            IUnitOfWork unitOfWork,
            IMediaService mediaService)
        {
            _unitOfWork = unitOfWork;
            _mediaService = mediaService;
        }

        public async Task<BaseResponse<string>> Handle(DeleteMarketCommand request, CancellationToken cancellationToken)
        {
            var market = await _unitOfWork.Repository<Market>().GetByIdAsync(request.Id);

            if (market == null)
            {
                return BaseResponse<string>.Fail($"Market not found.", HttpStatusCode.NotFound);
            }

            _mediaService.Delete(market.IconUrl);

            _unitOfWork.Repository<Market>().Delete(market);
            await _unitOfWork.SaveAsync();

            return BaseResponse<string>.Success("Market deleted.");
        }
    }
}
