using Fiker.Domain.Bases;
using Fiker.Domain.Domains.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Features.Authentication.ResetPassword
{
    public class ResetPasswordCommand:IRequest<BaseResponse<string>>
    {
        public string Password { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }

    internal class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, BaseResponse<string>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMemoryCache _memoryCache;

        public ResetPasswordCommandHandler(UserManager<User> userManager, IMemoryCache memoryCache)
        {
            _userManager = userManager;
            _memoryCache = memoryCache;
        }

        public async Task<BaseResponse<string>> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);

            if (user == null)
            {
                return BaseResponse<string>.Fail("No account with this email.");
            }

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, command.Token, command.Password);


            if (!resetPasswordResult.Succeeded)
            {
                return BaseResponse<string>.ValidationFailure(resetPasswordResult.Errors.ToList());
            }

            return BaseResponse<string>.Success(user.Id, "Password changed.");
        }
    }
}
