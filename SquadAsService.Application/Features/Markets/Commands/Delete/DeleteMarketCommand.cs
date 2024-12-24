using MediatR;
using SquadAsService.Application.Interfaces.Repo;
using SquadAsService.Domain.Bases;
using SquadAsService.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SquadAsService.Application.Features.Markets.Commands.Delete
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

        public DeleteMarketCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<string>> Handle(DeleteMarketCommand request, CancellationToken cancellationToken)
        {
            var market = await _unitOfWork.Repository<Market>().GetByIdAsync(request.Id);

            if (market == null)
            {
                return BaseResponse<string>.Fail($"Market not found.", HttpStatusCode.NotFound);
            }

            _unitOfWork.Repository<Market>().Delete(market);
            await _unitOfWork.SaveAsync();

            return BaseResponse<string>.Success("Market deleted.");
        }
    }
}
