using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Features.Admin.Commands.Create
{
    public class CreateAdminCommandValidator:AbstractValidator<CreateAdminCommand>
    {
        public CreateAdminCommandValidator()
        {
            RuleFor(x=>x.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
        }
    }
}
