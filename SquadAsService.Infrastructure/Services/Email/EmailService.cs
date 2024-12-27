﻿using Fiker.Application.Interfaces;
using Fiker.Domain.Dtos;
using FluentEmail.Core;
using FluentEmail.Core.Models;

namespace Fiker.Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmailFactory _fluentEmail;

        public EmailService(IFluentEmailFactory fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public async Task<bool> SendMailUsingRazorTemplateAsync(EmailRequestDto request)
        {
            var sendResponse = await _fluentEmail
                                        .Create()
                                        .SetFrom(request.From, "Fiker")
                                        .To(request.To)
                                        .Subject(request.Subject)
                                        .UsingTemplate(request.Body, request.BodyData)
                                        .SendAsync();

            return sendResponse.Successful;
        }
    }
}