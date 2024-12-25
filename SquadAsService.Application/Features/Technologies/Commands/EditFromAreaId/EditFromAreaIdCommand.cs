using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Bases;
using Fiker.Domain.Domains;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fiker.Application.Features.Technologies.Commands.RemoveFromAreaId
{
    public class EditFromAreaIdCommand : IRequest<BaseResponse<string>>
    {
        public int AreaId { get; set; }
        public int TechnologyId { get; set; }
    }

    internal class RemoveFromAreaIdCommandHandler : IRequestHandler<EditFromAreaIdCommand, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveFromAreaIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<string>> Handle(EditFromAreaIdCommand command, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<AreaTechonolgy>().Entities
                       .FirstOrDefaultAsync(x => x.AreaId == command.AreaId && x.TechnologyId == command.TechnologyId);

            if(entity is null)
            {
                await _unitOfWork.Repository<AreaTechonolgy>().AddAsync(new()
                {
                    AreaId = command.AreaId,
                    TechnologyId = command.TechnologyId
                });
            }
            else
            {
                _unitOfWork.Repository<AreaTechonolgy>().Delete(entity);
            }

            await _unitOfWork.SaveAsync();

            return BaseResponse<string>.Success();
        }
    }
}
