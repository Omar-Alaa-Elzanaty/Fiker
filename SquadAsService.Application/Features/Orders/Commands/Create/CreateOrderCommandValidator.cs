using FluentValidation;

namespace SquadAsService.Application.Features.Orders.Commands.Create
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.ContactName).NotEmpty().WithMessage("Contact Name is required.");
            RuleFor(x => x.ContactEmail).NotEmpty().WithMessage("Contact Email is required.");
            RuleFor(x => x.Telephone).NotEmpty().WithMessage("Telephone is required.");
            RuleFor(x => x.Profiles).Must(x => x.All(x => x.Quantity <= 20));
        }
    }
}
