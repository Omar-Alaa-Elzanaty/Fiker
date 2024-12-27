using FluentValidation;

namespace Fiker.Application.Features.Authentication.SendConfirmEmailOtp
{
    public class SendConfirmEmailOtpCommandValidator:AbstractValidator<SendConfirmEmailOtpCommand>
    {
        public SendConfirmEmailOtpCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
