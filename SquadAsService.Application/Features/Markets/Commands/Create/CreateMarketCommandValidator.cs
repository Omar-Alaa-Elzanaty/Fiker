using FluentValidation;

namespace SquadAsService.Application.Features.Markets.Commands.Create
{
    public class CreateMarketCommandValidator : AbstractValidator<CreateMarketCommand>
    {
        public CreateMarketCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(30);

            RuleFor(x => x.IconFile)
                .NotNull()
                .WithMessage("Icon is required.");
        }
    }
}
