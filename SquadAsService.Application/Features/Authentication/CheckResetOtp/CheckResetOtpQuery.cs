using Fiker.Domain.Bases;
using Fiker.Domain.Domains.Identity;
using FluentValidation;
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

namespace Fiker.Application.Features.Authentication.CheckResetOtp
{
    public class CheckResetOtpQuery:IRequest<BaseResponse<string>>
    {
        public string Email { get; set; }
        public int Otp { get; set; }
    }

    internal class CheckResetOtpQueryHandler : IRequestHandler<CheckResetOtpQuery, BaseResponse<string>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMemoryCache _memoryCache;
        private readonly IValidator<CheckResetOtpQuery> _validator;

        public CheckResetOtpQueryHandler(
            UserManager<User> userManager,
            IMemoryCache memoryCache,
            IValidator<CheckResetOtpQuery> validator)
        {
            _userManager = userManager;
            _memoryCache = memoryCache;
            _validator = validator;
        }

        public async Task<BaseResponse<string>> Handle(CheckResetOtpQuery query, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return BaseResponse<string>.ValidationFailure(validationResult.Errors.ToList(), HttpStatusCode.UnprocessableEntity);
            }

            var user = await _userManager.FindByEmailAsync(query.Email);

            if (user == null)
            {
                return BaseResponse<string>.Fail("No account with this email.");
            }

            var otp = _memoryCache.Get<int>("Reset" + user.Id);


            if (otp != query.Otp)
            {
                return BaseResponse<string>.Fail("Otp is incorrect.");
            }

            _memoryCache.Remove("Reset" + user.Id);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return BaseResponse<string>.Success(token, "Otp is correct.");
        }
    }
}
