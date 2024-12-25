using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Bases;
using Fiker.Domain.Domains;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Features.JobTitles.Commands.EditFromTechnologyId
{
    public class EditFromTechnologyIdCommand:IRequest<BaseResponse<string>>
    {
        public int TechnologyId { get; set; }
        public int JobTitleId { get; set; }
    }

    internal class EditFromTechnologyIdCommandHandler : IRequestHandler<EditFromTechnologyIdCommand, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditFromTechnologyIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<string>> Handle(EditFromTechnologyIdCommand command, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<TechnologyJobTitle>().Entities
                        .FirstOrDefaultAsync(x => x.TechnologyId == command.TechnologyId && x.JobTitleId == command.JobTitleId);

            if(entity == null)
            {
                await _unitOfWork.Repository<TechnologyJobTitle>().AddAsync(new()
                {
                    TechnologyId = command.TechnologyId,
                    JobTitleId = command.JobTitleId,
                });
            }
            else
            {
                _unitOfWork.Repository<TechnologyJobTitle>().Delete(entity);
            }

            await _unitOfWork.SaveAsync();

            return BaseResponse<string>.Success();
        }
    }
}
