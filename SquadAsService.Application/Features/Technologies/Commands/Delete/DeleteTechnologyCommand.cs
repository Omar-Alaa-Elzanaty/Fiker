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

namespace SquadAsService.Application.Features.Technologies.Commands.Delete
{
    public record DeleteTechnologyCommand:IRequest<BaseResponse<string>>
    {
        public int Id { get; set; }
        public DeleteTechnologyCommand(int id)
        {
            Id = id;
        }
    }

    internal class DeleteCommandHandler: IRequestHandler<DeleteTechnologyCommand, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<string>> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
        {
            var technology = await _unitOfWork.Repository<Technology>().GetByIdAsync(request.Id);

            if (technology == null)
            {
                return BaseResponse<string>.Fail($"Technology not found.", HttpStatusCode.NotFound);
            }

            _unitOfWork.Repository<Technology>().Delete(technology);
            await _unitOfWork.SaveAsync();

            return BaseResponse<string>.Success("Technology deleted.");
        }
    }
}