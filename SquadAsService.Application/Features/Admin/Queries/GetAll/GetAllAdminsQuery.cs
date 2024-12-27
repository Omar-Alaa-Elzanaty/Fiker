using Fiker.Domain.Bases;
using Fiker.Domain.Domains.Identity;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fiker.Application.Features.Admin.Queries.GetAll
{
    public record GetAllAdminsQuery : IRequest<BaseResponse<List<GetAllAdminsQueryDto>>>;

    internal class GetAllAdminsQueryHandler : IRequestHandler<GetAllAdminsQuery, BaseResponse<List<GetAllAdminsQueryDto>>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public GetAllAdminsQueryHandler(UserManager<User> userManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<BaseResponse<List<GetAllAdminsQueryDto>>> Handle(GetAllAdminsQuery query, CancellationToken cancellationToken)
        {
            var entities = await _userManager.Users.ToListAsync(cancellationToken);

            var currentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext!.User);

            var users = new List<GetAllAdminsQueryDto>();

            foreach (var entity in entities)
            {
                if (entity.Id == currentUser!.Id)
                    continue;

                var user = entity.Adapt<GetAllAdminsQueryDto>();
                user.Role = (await _userManager.GetRolesAsync(entity)).First();
                users.Add(user);
            }

            return BaseResponse<List<GetAllAdminsQueryDto>>.Success(users);
        }
    }
}
