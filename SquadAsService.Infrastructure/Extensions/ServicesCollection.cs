using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fiker.Application.Interfaces;
using Fiker.Infrastructure.Services.AuthServices;
using Fiker.Infrastructure.Services.MediaServices;
using System.Net.Mail;
using System.Net;
using Hangfire;
using Fiker.Infrastructure.Services.Email;
using Fiker.Infrastructure.Services.Job.Tasks;
using Fiker.Infrastructure.Services.RazorServices;
using Fiker.Infrastructure.Services.Report.Order;
using Fiker.Application.Features.Orders.Commands.Create;
using Fiker.Domain.Bases;
using MediatR;

namespace Fiker.Infrastructure.Extensions
{
    public static class ServicesCollection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddCollections()
                .AddHangFireServices(configuration)
                .AddFluentEmailServices(configuration);

            return services;
        }

        private static IServiceCollection AddCollections(this IServiceCollection services)
        {
            services.AddTransient<IMediaService, MediaService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ICategoryTasks, CategoryTasks>();
            //services.AddTransient<IRazorRendering,RazorRendering>();
            //services.AddTransient<IOrderReport, OrderReport>();

            return services;
        }

        private static IServiceCollection AddHangFireServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("DataBase")))
                    .AddHangfireServer();

            return services;
        }
        private static IServiceCollection AddFluentEmailServices(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection("MailSettings");
            var defaultFromEmail = emailSettings["SenderEmail"];
            var host = emailSettings["Server"];
            var port = emailSettings.GetValue<int>("Port");
            var userName = emailSettings["UserName"];
            var password = emailSettings["Password"];
            var enableSsl = emailSettings.GetValue<bool>("EnableSSL");
            var useDefaultCredentials = emailSettings.GetValue<bool>("UseDefaultCredentials");

            var smtpClient = new SmtpClient
            {
                EnableSsl = enableSsl,
                Host = host,
                Port = port,
                UseDefaultCredentials = useDefaultCredentials,
                Credentials = new NetworkCredential(userName, password)
            };

            services.AddFluentEmail(defaultFromEmail)
                    .AddRazorRenderer()
                    .AddLiquidRenderer()
                    .AddSmtpSender(smtpClient);

            return services;
        }
    }
}
