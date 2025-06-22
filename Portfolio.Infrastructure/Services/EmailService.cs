using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Portfolio.Core.Entities;
using Portfolio.Core.Interfaces.Services;
using Portfolio.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResultModel<string>> SendEmailAsync(EmailCreateRequestModel emailCreateRequestModel)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(emailCreateRequestModel.Name, emailCreateRequestModel.Email));
                message.To.Add(new MailboxAddress(_configuration["Smtp:User"], _configuration["Smtp:User"]));
                message.Subject = emailCreateRequestModel.Subject;
                message.Body = new TextPart("html") { Text = emailCreateRequestModel.Message };

                using var client = new MailKit.Net.Smtp.SmtpClient();
                await client.ConnectAsync(_configuration["Smtp:Host"], int.Parse(_configuration["Smtp:Port"]!), SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_configuration["Smtp:User"], _configuration["Smtp:Password"]);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return new ResultModel<string>
                {
                    Success = true,
                    Value = "Email sent successfully",
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<string>
                {
                    Success = false,
                    Errors = new List<string> {$"Failed to send email: {ex.Message}"},
                };
            }
        }
    }
}
