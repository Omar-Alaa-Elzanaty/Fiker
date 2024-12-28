using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Bases;
using Fiker.Domain.Domains;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fiker.Application.Features.Orders.Commands.Create
{
    public record CreateOrderCommand : IRequest<BaseResponse<int>>
    {
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string Telephone { get; set; }
        public string? Company { get; set; }
        public string? Question { get; set; }
        public int AreaId { get; set; }
        public int MarketId { get; set; }
        public int TechnologyId { get; set; }
        public bool Subscribe { get; set; }
        public List<OrderProfile> Profiles { get; set; }
    }
    public class OrderProfile
    {
        public string JobTitle { get; set; }
        public int Quantity { get; set; }

    }
    internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, BaseResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateOrderCommand> _validator;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IValidator<CreateOrderCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<BaseResponse<int>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var valdiationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!valdiationResult.IsValid)
            {
                return BaseResponse<int>.ValidationFailure(valdiationResult.Errors);
            }

            if (!await _unitOfWork.Repository<AreaTechonolgy>().Entities
                .AnyAsync(x => x.AreaId == command.AreaId && x.TechnologyId == command.TechnologyId, cancellationToken))
            {
                return BaseResponse<int>.Fail("Area and Technology are not related");
            }

            if (command.Subscribe
                && !await _unitOfWork.Repository<Subscriber>().Entities.AnyAsync(x => x.ContactEmail == command.ContactEmail, cancellationToken))
            {
                var subscriber = command.Adapt<Subscriber>();
                await _unitOfWork.Repository<Subscriber>().AddAsync(subscriber);
            }

            var order = command.Adapt<Order>();

            await _unitOfWork.Repository<Order>().AddAsync(order);
            await _unitOfWork.SaveAsync();

            return BaseResponse<int>.Success(order.Id);
        }
    }
}
