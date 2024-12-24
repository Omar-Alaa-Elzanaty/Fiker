using FluentValidation;

namespace SquadAsService.Application.Features.JobTitles.Commands.Create
{
    public class CreateJobTitlesCommandValidator : AbstractValidator<CreateJobTitlesCommand>
    {
        public CreateJobTitlesCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(30);
        }
    }
}
