using Fiker.Application.Interfaces;
using Fiker.Domain.Bases;
using Fiker.Domain.Domains.Identity;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace Fiker.Application.Features.Authentication.FogetPassword
{
    public class ForgetPasswordCommand : IRequest<BaseResponse<string>>
    {
        public string Email { get; set; }

        public ForgetPasswordCommand(string email)
        {
            Email = email;
        }
    }

    internal class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, BaseResponse<string>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IValidator<ForgetPasswordCommand> _validator;
        private readonly IEmailSender _emailSender;
        private readonly IMemoryCache _memoryCache;

        public ForgetPasswordCommandHandler(
            UserManager<User> userManager,
            IValidator<ForgetPasswordCommand> validator,
            IEmailSender emailSender,
            IMemoryCache memoryCache)
        {
            _userManager = userManager;
            _validator = validator;
            _emailSender = emailSender;
            _memoryCache = memoryCache;
        }

        public async Task<BaseResponse<string>> Handle(ForgetPasswordCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BaseResponse<string>.ValidationFailure(validationResult.Errors);
            }

            var user = await _userManager.FindByEmailAsync(command.Email);

            if (user is null)
            {
                return BaseResponse<string>.Fail("Email not found");
            }

            _memoryCache.Remove("Reset" + user.Id);

            int otp = await _memoryCache.GetOrCreateAsync(
                    key: "Reset" + user.Id,
                    cacheEntry =>
                    {
                        cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(3);
                        return Task.FromResult(new Random().Next(1000, 10000));
                    });

            var isEmailSent = await _emailSender.SendForgetPasswordEmailAsync(command.Email, user.FirstName + ' ' + user.LastName, otp);

            if (!isEmailSent)
            {
                _memoryCache.Remove(user.Id);
                return BaseResponse<string>.Fail($"Fail to send otp to {command.Email}");
            }

            return BaseResponse<string>.Fail($"Otp was sent to {command.Email}.");
        }
    }
}
