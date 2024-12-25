using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Domains.Identity;
using Fiker.Presistance.Context;
using Fiker.Presistance.Repo;

namespace Fiker.Presistance.Extensions
{
    public static class ServicesCollection
    {
        public static IServiceCollection AddPresistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddContext(configuration)
                    .AddCollections();
            return services;
        }
        private static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DataBase");

            services.AddDbContext<SquadDb>(options =>
               options.UseLazyLoadingProxies().UseSqlServer(connectionString,
                   builder => builder.MigrationsAssembly(typeof(SquadDb).Assembly.FullName)));

            // Identity configuration
            services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<SquadDb>()
                    .AddUserManager<UserManager<User>>()
                    .AddRoleManager<RoleManager<IdentityRole>>()
                    .AddSignInManager<SignInManager<User>>()
                    .AddSignInManager()
                    .AddDefaultTokenProviders();


            return services;
        }

        private static IServiceCollection AddCollections(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
