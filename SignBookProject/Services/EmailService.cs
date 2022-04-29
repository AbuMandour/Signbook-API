using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SignbookApi.Models;
using SignbookApi.Services.Interfaces;
using SignbookApi.Tools;
using System.Threading.Tasks;

namespace SignbookApi.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;

        public EmailService(IOptions<MailSettings> mailsettings)
        {
            _mailSettings = mailsettings.Value;
        }

        public async Task SendEmailAsync(EmailModel model)
        {
            var mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Email),
                Subject = model.UserName
            };

            string mailto = _mailSettings.Mailto;
            mail.To.Add(MailboxAddress.Parse(mailto));

            var builder = new BodyBuilder();
            string body = model.Message + model.Email;
            builder.HtmlBody = body;
            mail.Body = builder.ToMessageBody();
            mail.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Email, _mailSettings.Password);
            await smtp.SendAsync(mail);

            smtp.Dispose();
        }
    }
}
