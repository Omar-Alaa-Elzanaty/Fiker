using FluentValidation;

namespace Fiker.Application.Features.Markets.Commands.Create
{
    public class CreateMarketCommandValidator : AbstractValidator<CreateMarketCommand>
    {
        public CreateMarketCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(30);

            RuleFor(x => x.IconFile.FileName)
                .NotEmpty()
                .WithMessage("Icon is required.");
            
            RuleFor(x => x.IconFile.Base64)
                .NotEmpty()
                .WithMessage("Icon is required.");
        }
    }
}
