using Fiker.Domain.Bases;
using Fiker.Domain.Domains.Identity;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fiker.Application.Features.Admin.Commands.Create
{
    public class CreateAdminCommand : IRequest<BaseResponse<string>>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }

    internal class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, BaseResponse<string>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IValidator<CreateAdminCommand> _validator;

        public CreateAdminCommandHandler(UserManager<User> userManager, IValidator<CreateAdminCommand> validator)
        {
            _userManager = userManager;
            _validator = validator;
        }

        public async Task<BaseResponse<string>> Handle(CreateAdminCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BaseResponse<string>.ValidationFailure(validationResult.Errors);
            }

            if (await _userManager.Users.AnyAsync(x => x.Email == command.Email, cancellationToken))
            {
                return BaseResponse<string>.Fail("Email already used.");
            }

            if (await _userManager.Users.AnyAsync(x => x.UserName == command.UserName, cancellationToken))
            {
                return BaseResponse<string>.Fail("Username already used.");
            }

            var user = command.Adapt<User>();

            command.Role = command.Role.Replace(" ", "");

            var result = await _userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
            {
                return BaseResponse<string>.ValidationFailure(result.Errors.ToList());
            }

            var roleAddResult = await _userManager.AddToRoleAsync(user, command.Role);

            return BaseResponse<string>.Success(user.Id, "Admin Created.");
        }
    }
}
