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

namespace Fiker.Application.Features.Technologies.Commands.Delete
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
        private readonly IMediaService _mediaService;

        public DeleteCommandHandler(
            IUnitOfWork unitOfWork,
            IMediaService mediaService)
        {
            _unitOfWork = unitOfWork;
            _mediaService = mediaService;
        }

        public async Task<BaseResponse<string>> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
        {
            var technology = await _unitOfWork.Repository<Technology>().GetByIdAsync(request.Id);

            if (technology == null)
            {
                return BaseResponse<string>.Fail($"Technology not found.", HttpStatusCode.NotFound);
            }

            _mediaService.Delete(technology.IconUrl);

            _unitOfWork.Repository<Technology>().Delete(technology);
            await _unitOfWork.SaveAsync();

            return BaseResponse<string>.Success("Technology deleted.");
        }
    }
}