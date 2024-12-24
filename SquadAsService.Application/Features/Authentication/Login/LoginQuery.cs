using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SquadAsService.Application.Interfaces;
using SquadAsService.Domain.Bases;
using SquadAsService.Domain.Domains.Identity;
using System.Net;

namespace SquadAsService.Application.Features.Authentication.Login
{
    public class LoginQuery : IRequest<BaseResponse<LoginQueryDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    internal class LoginQueryHandler : IRequestHandler<LoginQuery, BaseResponse<LoginQueryDto>>
    {
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LoginQueryHandler(
            IAuthService authService,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _authService = authService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<BaseResponse<LoginQueryDto>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            query.UserName = query.UserName.Trim();
            query.Password = query.Password.Trim();

            var user = await _userManager.FindByNameAsync(query.UserName);

            if (user is null)
            {
                return BaseResponse<LoginQueryDto>.Fail("Invalid username or password", HttpStatusCode.Unauthorized);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(query.UserName, query.Password, query.RememberMe, true);

            if (!signInResult.Succeeded && signInResult.IsLockedOut)
            {
                return BaseResponse<LoginQueryDto>.Fail("Account locked for while,Please try again later.", HttpStatusCode.Forbidden);
            }
            else if (!signInResult.Succeeded)
            {
                return BaseResponse<LoginQueryDto>.Fail("Invalid username or password", HttpStatusCode.Unauthorized);
            }

            var loginResponse = user.Adapt<LoginQueryDto>();

            loginResponse.Role = (await _userManager.GetRolesAsync(user)).First();

            loginResponse.Token = _authService.GenerateToken(user, loginResponse.Role, query.RememberMe);

            return BaseResponse<LoginQueryDto>.Success(loginResponse);
        }
    }
}
