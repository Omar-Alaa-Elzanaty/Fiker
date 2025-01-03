using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Fiker.Application.Extensions;
using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Bases;
using Fiker.Domain.Domains;
using Hangfire;
using Fiker.Application.Interfaces;

namespace Fiker.Application.Features.JobTitles.Commands.Create
{
    public record CreateJobTitlesCommand : IRequest<BaseResponse<int>>
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }

    internal class CreateJobTitlesCommandHandler : IRequestHandler<CreateJobTitlesCommand, BaseResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryTasks _categoryTasks;
        private readonly IValidator<CreateJobTitlesCommand> _validator;

        public CreateJobTitlesCommandHandler(
            IUnitOfWork unitOfWork,
            IValidator<CreateJobTitlesCommand> validator,
            ICategoryTasks categoryTasks)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _categoryTasks = categoryTasks;
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

            //BackgroundJob.Enqueue(() => _categoryTasks.SendNewProfile(command.Name).GetAwaiter());


            return BaseResponse<int>.Success(jobTitle.Id);
        }
    }
}
