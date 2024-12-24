using MediatR;
using SquadAsService.Application.Interfaces.Repo;
using SquadAsService.Domain.Bases;
using SquadAsService.Domain.Domains;
using System.Net;

namespace SquadAsService.Application.Features.Areas.Commands.Delete
{
    public class DeleteAreaCommand : IRequest<BaseResponse<string>>
    {
        public int Id { get; set; }

        public DeleteAreaCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteAreaCommandHandler : IRequestHandler<DeleteAreaCommand, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAreaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<string>> Handle(DeleteAreaCommand request, CancellationToken cancellationToken)
        {
            var area = await _unitOfWork.Repository<Area>().GetByIdAsync(request.Id);

            if (area == null)
            {
                return BaseResponse<string>.Fail($"Area not found.", HttpStatusCode.NotFound);
            }

            _unitOfWork.Repository<Area>().Delete(area);
            await _unitOfWork.SaveAsync();

            return BaseResponse<string>.Success($"Area deleted.");
        }
    }
}
