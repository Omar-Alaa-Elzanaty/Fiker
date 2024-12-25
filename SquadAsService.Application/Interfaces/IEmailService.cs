using Fiker.Domain.Dtos;

namespace Fiker.Application.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendMailUsingRazorTemplateAsync(EmailRequestDto request);
    }
}
