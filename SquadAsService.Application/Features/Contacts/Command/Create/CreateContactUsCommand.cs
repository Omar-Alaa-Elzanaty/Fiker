using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Bases;
using Fiker.Domain.Domains;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fiker.Application.Features.Contacts.Command.Create
{
    public record CreateContactUsCommand : IRequest<BaseResponse<int>>
    {
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string Telphone { get; set; }
        public string Company { get; set; }
        public string? Question { get; set; }
        public bool Subscribe { get; set; }
    }

    internal class CreateContactUsCommandHandler : IRequestHandler<CreateContactUsCommand, BaseResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateContactUsCommand> _validator;

        public CreateContactUsCommandHandler(
            IUnitOfWork unitOfWork,
            IValidator<CreateContactUsCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<BaseResponse<int>> Handle(CreateContactUsCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BaseResponse<int>.ValidationFailure(validationResult.Errors);
            }

            var contactUs = command.Adapt<ContactUs>();

            if (command.Subscribe 
                && await _unitOfWork.Repository<Subscriber>().Entities.AnyAsync(x => x.ContactEmail == command.ContactEmail,cancellationToken))
            {
                var subscriber = command.Adapt<Subscriber>();
                await _unitOfWork.Repository<Subscriber>().AddAsync(subscriber);
            }

            await _unitOfWork.Repository<ContactUs>().AddAsync(contactUs);
            await _unitOfWork.SaveAsync();

            return BaseResponse<int>.Success(contactUs.Id);
        }
    }
}
