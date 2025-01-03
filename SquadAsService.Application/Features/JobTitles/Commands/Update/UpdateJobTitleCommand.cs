using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Bases;
using Fiker.Domain.Domains;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace Fiker.Application.Features.JobTitles.Commands.Update
{
    public class UpdateJobTitleCommand : IRequest<BaseResponse<int>>
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }

    internal class UpdateJobTitleCommandHandler : IRequestHandler<UpdateJobTitleCommand, BaseResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateJobTitleCommand> _validator;
        private readonly IMapper _mapper;

        public UpdateJobTitleCommandHandler(
            IUnitOfWork unitOfWork,
            IValidator<UpdateJobTitleCommand> validator,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<BaseResponse<int>> Handle(UpdateJobTitleCommand command, CancellationToken cancellationToken)
        {
            var validtionResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validtionResult.IsValid)
            {
                return BaseResponse<int>.ValidationFailure(validtionResult.Errors);
            }

            var jobTitle = await _unitOfWork.Repository<JobTitle>().GetByIdAsync(command.id);

            if (jobTitle == null)
            {
                return BaseResponse<int>.Fail($"Job Title  not found");
            }

            _mapper.Map(command, jobTitle);

            _unitOfWork.Repository<JobTitle>().UpdateAsync(jobTitle);
            await _unitOfWork.SaveAsync();

            return BaseResponse<int>.Success(jobTitle.Id);
        }
    }
}
