using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Constants;
using Fiker.Domain.Domains.Identity;
using Fiker.Presistance.Context;

namespace Fiker.Presistance.Seeding
{
    public class SeedingData
    {
        public static async Task Invoke(IServiceProvider service)
        {
            var dbContext = service.GetRequiredService<SquadDb>();
            var userManager = service.GetRequiredService<UserManager<User>>();
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            var applicationMigrations = dbContext.Database.GetPendingMigrations();

            if (applicationMigrations.Count() == 0)
            {
                dbContext.Database.Migrate();

                var unitOfWork = service.GetRequiredService<IUnitOfWork>();

                await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin));
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));

                var admin = new User
                {
                    UserName = "Admin",
                    FirstName = "Omar",
                    LastName = "Alaa"
                };

                await userManager.CreateAsync(admin, "Admin@123");

                await userManager.AddToRoleAsync(admin, Roles.SuperAdmin);
            }
        }
    }
}
