using Fiker.Application.Interfaces;
using Fiker.Domain.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Fiker.Infrastructure.Services.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _filePath;
        private readonly string _email;

        public EmailSender(IEmailService emailService, IWebHostEnvironment host, IConfiguration config)
        {
            _emailService = emailService;
            _host = host;
            _filePath = config.GetSection("EmailTemplates");
            _email = config.GetSection("MailSettings:SenderEmail").Value!;
        }

        public async Task<bool> SendNewMarketEmail(string marketName, List<string> email)
        {
            var content = File.ReadAllText(_host.WebRootPath + _filePath["NewMarket"]);

            return await _emailService.SendMailUsingRazorTemplateAsync(new EmailRequestDto
            {
                To = email,
                Subject = "Fiker - Markets",
                Body = content,
                From = _email,
                BodyData = new
                {
                    Market = marketName,
                    FikerEmail = _email
                }
            });
        }

        public async Task<bool> SendNewTechnologyEmail(string technologyName, List<string> email)
        {
            var content = File.ReadAllText(_host.WebRootPath + _filePath["NewTechnology"]);

            return await _emailService.SendMailUsingRazorTemplateAsync(new EmailRequestDto
            {
                To = email,
                Subject = "Fiker - Technologies",
                Body = content,
                From = _email,
                BodyData = new
                {
                    Technology = technologyName,
                    FikerEmail = _email
                }
            });
        }

        public async Task<bool> SendNewAreaEmail(string areaName, List<string> email)
        {
            var content = File.ReadAllText(_host.WebRootPath + _filePath["NewArea"]);

            return await _emailService.SendMailUsingRazorTemplateAsync(new EmailRequestDto
            {
                To = email,
                Subject = "Fiker - Areas",
                Body = content,
                From = _email,
                BodyData = new
                {
                    Area = areaName,
                    FikerEmail = _email
                }
            });
        }

        public async Task<bool> SendNewProfileEmail(string profileName, List<string> email)
        {
            var content = File.ReadAllText(_host.WebRootPath + _filePath["NewProfile"]);

            return await _emailService.SendMailUsingRazorTemplateAsync(new EmailRequestDto
            {
                To = email,
                Subject = "Fiker - Profiles",
                Body = content,
                From = _email,
                BodyData = new
                {
                    Profile = profileName,
                    FikerEmail = _email
                }
            });
        }

        public async Task<bool> SendForgetPasswordEmailAsync(string email, string name, int otp)
        {
            var content = File.ReadAllText(_host.WebRootPath + _filePath["OtpResetPassword"]);

            return await _emailService.SendMailUsingRazorTemplateAsync(new EmailRequestDto()
            {
                To = [email!],
                From = _email,
                Subject = "Fiker - Forget Password Otp",
                Body = content,
                BodyData = new
                {
                    Name = name,
                    Otp = otp
                }
            });
        }
        public async Task<bool> SendEmailConfirmationAsync(string email, int otp)
        {
            var content = File.ReadAllText(_host.WebRootPath + _filePath["EmailConfirmation"]);

            return await _emailService.SendMailUsingRazorTemplateAsync(new EmailRequestDto()
            {
                To = [email!],
                From = _email,
                Subject = "Fiker - Email Confirmation Otp",
                Body = content,
                BodyData = new
                {
                    Otp = otp
                }
            });
        }

        public async Task<bool> SendOrderReportEmailAsync(string email, string copmany, MediaFormFileDto attachmentReport)
        {
            var content = File.ReadAllText(_host.WebRootPath + _filePath["OrderReport"]);

            return await _emailService.SendMailUsingRazorTemplateAsync(new EmailRequestDto()
            {
                To = [email!],
                From = _email,
                Subject = "Fiker - Email Confirmation Otp",
                Body = content,
                Attachment = attachmentReport,
                BodyData = new
                {
                    Company = copmany,
                    FikerEmail = _email
                }
            });
        }
    }
}
