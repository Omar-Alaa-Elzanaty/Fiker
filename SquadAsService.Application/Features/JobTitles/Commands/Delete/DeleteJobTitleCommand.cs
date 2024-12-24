using MediatR;
using SquadAsService.Application.Interfaces.Repo;
using SquadAsService.Domain.Bases;
using SquadAsService.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadAsService.Application.Features.JobTitles.Commands.Delete
{
    public record DeleteJobTitleCommand:IRequest<BaseResponse<string>>
    {
        public int Id { get; set; }

        public DeleteJobTitleCommand(int id)
        {
            Id = id;
        }
    }

    internal class DeleteJobTitleCommandHandler: IRequestHandler<DeleteJobTitleCommand, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteJobTitleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<string>> Handle(DeleteJobTitleCommand request, CancellationToken cancellationToken)
        {
            var jobTitle = await _unitOfWork.Repository<JobTitle>().GetByIdAsync(request.Id);

            if (jobTitle == null)
            {
                return BaseResponse<string>.Fail($"Job title not found.", System.Net.HttpStatusCode.NotFound);
            }

            _unitOfWork.Repository<JobTitle>().Delete(jobTitle);
            await _unitOfWork.SaveAsync();

            return BaseResponse<string>.Success($"Job title deleted.");
        }
    }
}
