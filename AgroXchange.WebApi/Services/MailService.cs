using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;
using AgroXchange.WebApi.Helpers;
using AgroXchange.WebApi.Models;

namespace AgroXchange.WebApi.Services
{
    public interface IMailService
    {
        void SendMail(Mail mail);
    }

    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public void SendMail(Mail mail)
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress(_mailSettings.FromName, _mailSettings.FromEmail);
            message.From.Add(from);

            foreach (string emailId in mail.To.Keys)
            {
                MailboxAddress to = new MailboxAddress(mail.To[emailId], emailId);
                message.To.Add(to);
            }

            if (mail.Cc != null)
            {
                foreach (string emailId in mail.Cc.Keys)
                {
                    MailboxAddress cc = new MailboxAddress(mail.Cc[emailId], emailId);
                    message.Cc.Add(cc);
                }
            }

            message.Subject = mail.Subject;

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = mail.BodyHtml;
            bodyBuilder.TextBody = "";

            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            client.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, true);
            client.Authenticate(_mailSettings.SmtpUsername, _mailSettings.SmtpPassword);

            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}
