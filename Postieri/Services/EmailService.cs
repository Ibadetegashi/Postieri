﻿using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Postieri.DTOs;

namespace Postieri.Interfaces
{

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(EmailDto request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(request.From));
            email.To.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();

            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EamilPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
