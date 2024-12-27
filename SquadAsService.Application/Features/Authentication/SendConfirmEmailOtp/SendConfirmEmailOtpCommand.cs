using Fiker.Application.Interfaces;
using Fiker.Domain.Bases;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Features.Authentication.SendConfirmEmailOtp
{
    public class SendConfirmEmailOtpCommand:IRequest<BaseResponse<string>>
    {
        public string Email { get; set; }

        public SendConfirmEmailOtpCommand(string email)
        {
            Email = email;
        }
    }

    internal class SendConfirmEmailOtpCommandHandler : IRequestHandler<SendConfirmEmailOtpCommand, BaseResponse<string>>
    {
        private readonly IEmailSender _emailSender;
        private readonly IMemoryCache _cache;
        private readonly IValidator<SendConfirmEmailOtpCommand> _validator;
        public SendConfirmEmailOtpCommandHandler(
            IMemoryCache cache,
            IEmailSender emailSender,
            IValidator<SendConfirmEmailOtpCommand> validate)
        {
            _cache = cache;
            _emailSender = emailSender;
            _validator = validate;
        }

        public async Task<BaseResponse<string>> Handle(SendConfirmEmailOtpCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return BaseResponse<string>.ValidationFailure(validationResult.Errors.ToList(), HttpStatusCode.UnprocessableEntity);
            }

            var rand = new Random();
            var otp = rand.Next(1000, 10000);

            if (!await _emailSender.SendEmailConfirmationAsync(command.Email, otp))
            {
                return BaseResponse<string>.Fail("Couldn't send email confirmation otp");
            }

            _cache.Remove("Confirm" + command.Email);
            _cache.Set("Confirm" + command.Email, otp.ToString(), TimeSpan.FromMinutes(5));

            return BaseResponse<string>.Success("Otp sent to email.");
        }
    }
}
