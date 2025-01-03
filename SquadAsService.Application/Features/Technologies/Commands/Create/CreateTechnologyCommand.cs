using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Fiker.Application.Extensions;
using Fiker.Application.Interfaces;
using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Bases;
using Fiker.Domain.Domains;
using Fiker.Domain.Dtos;
using Hangfire;

namespace Fiker.Application.Features.Technologies.Commands.Create
{
    public record CreateTechnologyCommand : IRequest<BaseResponse<int>>
    {
        public string Name { get; set; }
        public MediaFile IconFile { get; set; }
        public List<int>? JobTitlesIds { get; set; }
    }

    internal class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, BaseResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediaService _mediaService;
        private readonly ICategoryTasks _categoryTasks;
        private readonly IValidator<CreateTechnologyCommand> _validator;

        public CreateTechnologyCommandHandler(
            IUnitOfWork unitOfWork,
            IValidator<CreateTechnologyCommand> validator,
            IMediaService mediaService,
            ICategoryTasks categoryTasks)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mediaService = mediaService;
            _categoryTasks = categoryTasks;
        }

        public async Task<BaseResponse<int>> Handle(CreateTechnologyCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BaseResponse<int>.ValidationFailure(validationResult.Errors);
            }

            if (await _unitOfWork.Repository<Technology>().Entities.AnyAsync(x => x.Name.ToLower().Replace(" ", "") == command.Name.SearchingFormat()))
            {
                return BaseResponse<int>.Fail($"Technology with name {command.Name} already exists");
            }

            var technology = command.Adapt<Technology>();

            if (command.JobTitlesIds != null && command.JobTitlesIds.Count > 0)
            {

                var areas = await _unitOfWork.Repository<JobTitle>().Entities
                    .CountAsync(x => command.JobTitlesIds.Contains(x.Id));

                if (areas != command.JobTitlesIds.Count)
                {
                    return BaseResponse<int>.Fail("Some of the areas are not valid");
                }

                technology.JobTitles = command.JobTitlesIds.Select(x => new TechnologyJobTitle()
                {
                    JobTitleId = x
                }).ToList();
            }

            technology.IconUrl = await _mediaService.Save(command.IconFile)!;

            await _unitOfWork.Repository<Technology>().AddAsync(technology);
            await _unitOfWork.SaveAsync();

            //BackgroundJob.Enqueue(() => _categoryTasks.SendNewTechnology(command.Name).GetAwaiter());

            return BaseResponse<int>.Success(technology.Id);
        }
    }
}
