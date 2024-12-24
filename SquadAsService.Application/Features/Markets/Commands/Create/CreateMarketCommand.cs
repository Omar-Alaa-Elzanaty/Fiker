using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SquadAsService.Application.Extensions;
using SquadAsService.Application.Interfaces;
using SquadAsService.Application.Interfaces.Repo;
using SquadAsService.Domain.Bases;
using SquadAsService.Domain.Domains;
using SquadAsService.Domain.Dtos;

namespace SquadAsService.Application.Features.Markets.Commands.Create
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
        private readonly IValidator<CreateMarketCommand> _validator;

        public CreateMarketCommandHandler(
            IUnitOfWork unitOfWork,
            IValidator<CreateMarketCommand> validator,
            IMediaService mediaService)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mediaService = mediaService;
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

            return BaseResponse<int>.Success(market.Id);
        }
    }
}
