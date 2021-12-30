using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace Groot.Service.Common
{
    public class MailSender : IMailSender
    {
        public readonly AppSettings appSettings;

        public MailSender(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public bool Send(string subject, string body, List<string> addresses)
        {
            try
            {
                if (addresses == null || !addresses.Any(x => !string.IsNullOrWhiteSpace(x))) return false;

                using (var smtpClient = new SmtpClient
                {
                    Host = appSettings.MailServer,
                    Port = appSettings.MailPort,
                    EnableSsl = true,
                    Credentials = new System.Net.NetworkCredential(appSettings.MailSendFrom, appSettings.MailPassword)
                })
                {
                    var mailMessage = new MailMessage
                    {
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true,
                        From = new MailAddress(appSettings.MailSendFrom, appSettings.MailSendFromName)
                    };

                    foreach (var mailAddress in addresses)
                    {
                        mailMessage.To.Add(mailAddress.Trim());
                    }

                    smtpClient.Send(mailMessage);

                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
