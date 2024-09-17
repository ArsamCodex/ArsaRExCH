using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;
using ArsaRExCH.Data;

namespace ArsaRExCH.Interface
{
    public class EmailSender2 : IEmailSender<ApplicationUser>
    {
        private readonly IConfiguration _configuration;

        public EmailSender2(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(ApplicationUser user, string subject, string htmlMessage)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");
            var mail = emailSettings["EmailAddress"];
            var pass = emailSettings["Password"];

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pass)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(mail),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            mailMessage.To.Add(user.Email);

            return client.SendMailAsync(mailMessage);
        }
    }
}
