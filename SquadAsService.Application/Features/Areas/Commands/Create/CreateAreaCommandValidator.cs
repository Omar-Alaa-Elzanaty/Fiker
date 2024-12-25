using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Features.Areas.Commands.Create
{
    public class CreateAreaCommandValidator : AbstractValidator<CreateAreaCommand>
    {
        public CreateAreaCommandValidator()
        {
            RuleFor(x=>x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(30);
        }
    }
}
