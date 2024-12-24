using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SquadAsService.Application.Interfaces;
using SquadAsService.Infrastructure.Services.AuthServices;
using SquadAsService.Infrastructure.Services.MediaServices;

namespace SquadAsService.Infrastructure.Extensions
{
    public static class ServicesCollection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCollections();

            return services;
        }

        private static IServiceCollection AddCollections(this IServiceCollection services)
        {
            services.AddTransient<IMediaService, MediaService>();
            services.AddTransient<IAuthService, AuthService>();

            return services;
        }
    }
}
