using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Bases;
using Fiker.Domain.Domains.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Features.Admin.Commands.Delete
{
    public class DeleteAdminCommand:IRequest<BaseResponse<string>>
    {
        public string Id { get; set; }

        public DeleteAdminCommand(string id)
        {
            Id = id;
        }
    }

    internal class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand, BaseResponse<string>>
    {
        private readonly UserManager<User> _userManager;

        public DeleteAdminCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<BaseResponse<string>> Handle(DeleteAdminCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.Id);

            if (user == null)
            {
                return BaseResponse<string>.Fail("User not found.", HttpStatusCode.NotFound);
            }

            await _userManager.DeleteAsync(user);

            return BaseResponse<string>.Success();
        }
    }
}
