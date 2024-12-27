using Fiker.Domain.Bases;
using Fiker.Domain.Constants;
using Fiker.Domain.Domains.Identity;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

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

            if(!validationResult.IsValid)
            {
                return BaseResponse<string>.ValidationFailure(validationResult.Errors);
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
