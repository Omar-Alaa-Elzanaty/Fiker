using Fiker.Application.Comman.Dtos;
using Fiker.Application.Interfaces;
using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Bases;
using Fiker.Domain.Domains;
using Fiker.Domain.Dtos;
using FluentValidation;
using Hangfire;
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
        public int MonthsCount { get; set; }
        public MediaFormFileDto Attachment { get; set; }
        public List<OrderProfile> Profiles { get; set; }
    }
    public class OrderProfile
    {
        public string JobTitle { get; set; }
        public int Quantity { get; set; }

    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, BaseResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        //private readonly IOrderReport _orderReport;
        private readonly IValidator<CreateOrderCommand> _validator;

        public CreateOrderCommandHandler(
            IUnitOfWork unitOfWork,
            IValidator<CreateOrderCommand> validator,
            IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _emailSender = emailSender;
            //_orderReport = orderReport;
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

            var jobTitles = await _unitOfWork.Repository<JobTitle>().Entities
                .Where(x => command.Profiles.Select(x => x.JobTitle).Contains(x.Name))
                .ToListAsync(cancellationToken);

            if(jobTitles.Count != command.Profiles.Count)
            {
                return BaseResponse<int>.Fail("Some job titles are not valid");
            }

            if (command.Subscribe
                && !await _unitOfWork.Repository<Subscriber>().Entities.AnyAsync(x => x.ContactEmail == command.ContactEmail, cancellationToken))
            {
                var subscriber = command.Adapt<Subscriber>();
                await _unitOfWork.Repository<Subscriber>().AddAsync(subscriber);
            }

            var order = command.Adapt<Order>();

            var orderReport = order.Adapt<OrderReportDto>();

            foreach(var profileCommand in command.Profiles)
            {
                var profile = jobTitles.First(x => x.Name == profileCommand.JobTitle);

                order.Cost += profile.Price * profileCommand.Quantity;
                orderReport.Jobs.Add(new JobTitleDto()
                {
                    Price = profile.Price,
                    Quantity = profileCommand.Quantity,
                    Title = profile.Name
                });
            }

            await _unitOfWork.Repository<Order>().AddAsync(order);
            await _unitOfWork.SaveAsync();

            await _emailSender.SendOrderReportEmailAsync(command.ContactEmail, command.Company ?? command.ContactName, command.Attachment);

            return BaseResponse<int>.Success(order.Id);
        }
    }
}
