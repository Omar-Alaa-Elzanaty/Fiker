﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Features.Technologies.Commands.Create
{
    public class CreateTechnologyCommandValidator : AbstractValidator<CreateTechnologyCommand>
    {
        public CreateTechnologyCommandValidator()
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
