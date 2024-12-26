using Fiker.Domain.Bases;
using Fiker.Domain.Domains.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Fiker.Application.Features.Admin.Commands.UpdateRole
{
    public class UpdateRoleCommand : IRequest<BaseResponse<string>>
    {
        public string UserId { get; set; }
        public string Role { get; set; }
    }

    internal class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, BaseResponse<string>>
    {
        private readonly UserManager<User> _userManager;

        public UpdateRoleCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<BaseResponse<string>> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.UserId);

            if (user == null)
            {
                return BaseResponse<string>.Fail("User not found.", HttpStatusCode.NotFound);
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRoleAsync(user,command.Role);

            return BaseResponse<string>.Success();
        }
    }
}
