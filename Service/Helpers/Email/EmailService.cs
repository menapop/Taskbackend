using DTOS.Dto;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.IO;
using System.Linq;


namespace Service.Helpers.Email
{
    public class EmailService : IEmailServie
    {
        private readonly IWebHostEnvironment _env;

        private readonly AppSettings _appSettings;

        public EmailService(IWebHostEnvironment env, IOptions<AppSettings> option)
        {
            _env = env;
            _appSettings = option.Value;
        }


        public async void SendEmail(EmailDto email)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_appSettings.EmailSetting.Email));
            message.To.Add(new MailboxAddress(email.Receiver));
            message.Subject = email.Subject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = File.ReadAllText(_env.WebRootPath + @"\Htmls\email.html").Replace("[0]", String.Join("  ", "".ToString().ToList()));
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(_appSettings.EmailSetting.EmailHost, Convert.ToInt32(_appSettings.EmailSetting.Port),true);
                client.AuthenticationMechanisms.Remove("XOAUTH");
                client.Authenticate(_appSettings.EmailSetting.Email, _appSettings.EmailSetting.EmailPassword);
                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }
    }
}
