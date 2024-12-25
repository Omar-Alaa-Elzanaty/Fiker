using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Fiker.Application.Extensions;
using Fiker.Application.Interfaces;
using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Bases;
using Fiker.Domain.Domains;
using Fiker.Domain.Dtos;
using Hangfire;

namespace Fiker.Application.Features.Markets.Commands.Create
{
    public record CreateMarketCommand : IRequest<BaseResponse<int>>
    {
        public string Name { get; set; }
        public MediaFile IconFile { get; set; }
    }

    internal class CreateMarketCommandHandler : IRequestHandler<CreateMarketCommand, BaseResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediaService _mediaService;
        private readonly ICategoryTasks _categoryTasks;
        private readonly IValidator<CreateMarketCommand> _validator;

        public CreateMarketCommandHandler(
            IUnitOfWork unitOfWork,
            IValidator<CreateMarketCommand> validator,
            IMediaService mediaService,
            ICategoryTasks categoryTasks)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mediaService = mediaService;
            _categoryTasks = categoryTasks;
        }

        public async Task<BaseResponse<int>> Handle(CreateMarketCommand command, CancellationToken cancellationToken)
        {
            var validtionResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validtionResult.IsValid)
            {
                return BaseResponse<int>.ValidationFailure(validtionResult.Errors);
            }

            if(await _unitOfWork.Repository<Market>().Entities.AnyAsync(x=>x.Name.ToLower().Replace(" ", "") == command.Name.SearchingFormat()))
            {
                return BaseResponse<int>.Fail($"Market with name {command.Name} already exists");
            }

            var market = command.Adapt<Market>();

            market.IconUrl = await _mediaService.Save(command.IconFile);

            await _unitOfWork.Repository<Market>().AddAsync(market);
            await _unitOfWork.SaveAsync();

            BackgroundJob.Enqueue(() => _categoryTasks.SendNewMarket(command.Name).GetAwaiter());


            return BaseResponse<int>.Success(market.Id);
        }
    }
}
