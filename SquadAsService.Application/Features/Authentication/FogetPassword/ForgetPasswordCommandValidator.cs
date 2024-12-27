using FluentValidation;

namespace Fiker.Application.Features.Authentication.FogetPassword
{
    public class ForgetPasswordCommandValidator:AbstractValidator<ForgetPasswordCommand>
    {
        public ForgetPasswordCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
