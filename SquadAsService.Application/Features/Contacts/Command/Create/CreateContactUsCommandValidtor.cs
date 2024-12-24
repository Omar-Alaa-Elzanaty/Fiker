using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadAsService.Application.Features.Contacts.Command.Create
{
    public class CreateContactUsCommandValidtor : AbstractValidator<CreateContactUsCommand>
    {
        public CreateContactUsCommandValidtor()
        {
            RuleFor(x => x.ContactEmail).EmailAddress();
        }
    }
}
