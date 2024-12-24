using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SquadAsService.Application.Extensions;
using SquadAsService.Application.Interfaces.Repo;
using SquadAsService.Domain.Bases;
using SquadAsService.Domain.Domains;

namespace SquadAsService.Application.Features.JobTitles.Commands.Create
{
    public record CreateJobTitlesCommand : IRequest<BaseResponse<int>>
    {
        public string Name { get; set; }
    }

    internal class CreateJobTitlesCommandHandler : IRequestHandler<CreateJobTitlesCommand, BaseResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateJobTitlesCommand> _validator;

        public CreateJobTitlesCommandHandler(
            IUnitOfWork unitOfWork,
            IValidator<CreateJobTitlesCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<BaseResponse<int>> Handle(CreateJobTitlesCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BaseResponse<int>.ValidationFailure(validationResult.Errors);
            }

            if (await _unitOfWork.Repository<JobTitle>().Entities.AnyAsync(x => x.Name.ToLower().Replace(" ", "") == command.Name.SearchingFormat()))
            {
                return BaseResponse<int>.Fail($"Job Title with name {command.Name} already exists");
            }

            var jobTitle = command.Adapt<JobTitle>();

            await _unitOfWork.Repository<JobTitle>().AddAsync(jobTitle);
            await _unitOfWork.SaveAsync();

            return BaseResponse<int>.Success(jobTitle.Id);
        }
    }
}
