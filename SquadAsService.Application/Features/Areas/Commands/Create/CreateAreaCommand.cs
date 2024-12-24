using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SquadAsService.Application.Extensions;
using SquadAsService.Application.Interfaces.Repo;
using SquadAsService.Domain.Bases;
using SquadAsService.Domain.Domains;

namespace SquadAsService.Application.Features.Areas.Commands.Create
{
    public record CreateAreaCommand : IRequest<BaseResponse<int>>
    {
        public string Name { get; set; }
        public List<int>? TechnologiesIds { get; set; }
    }

    internal class CreateAreaCommandHandler : IRequestHandler<CreateAreaCommand, BaseResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateAreaCommand> _validator;

        public CreateAreaCommandHandler(
            IUnitOfWork unitOfWork,
            IValidator<CreateAreaCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<BaseResponse<int>> Handle(CreateAreaCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BaseResponse<int>.ValidationFailure(validationResult.Errors);
            }


            if (await _unitOfWork.Repository<Area>().Entities.AnyAsync(x => x.Name.ToLower().Replace(" ", "") == command.Name.SearchingFormat()))
            {
                return BaseResponse<int>.Fail($"Area with name {command.Name} already exists");
            }

            var area = command.Adapt<Area>();

            if (command.TechnologiesIds != null && command.TechnologiesIds.Count > 0)
            {
                var technologies = await _unitOfWork.Repository<Technology>().Entities
                    .CountAsync(x => command.TechnologiesIds.Contains(x.Id));

                if (technologies != command.TechnologiesIds.Count)
                {
                    return BaseResponse<int>.Fail("Some of the technologies are not valid");
                }


                area.Techonolgies = command.TechnologiesIds.Select(x => new AreaTechonolgy()
                {
                    TechnologyId = x
                }).ToList();
            }

            await _unitOfWork.Repository<Area>().AddAsync(area);
            await _unitOfWork.SaveAsync();

            return BaseResponse<int>.Success(area.Id);
        }
    }
}
