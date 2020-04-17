using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MantiScanServices.Common.Notification
{
    public class SendNotification
    {
        private readonly AnraConfiguration configuration;
        private readonly ILogger<SendNotification> _logger;

        public SendNotification(AnraConfiguration configuration, ILogger<SendNotification> logger)
        {
            this.configuration = configuration;
            _logger = logger;
        }

        public async void SendEmail(string emailAddress, string emailBody, string emailSubject)
        {
            try
            {
                MailMessage msg = new MailMessage
                {
                    From = new MailAddress(configuration.SendersEmail)
                };
                msg.To.Add(emailAddress);
                msg.Subject = emailSubject;
                msg.Body = emailBody;
                msg.IsBodyHtml = true;

                SmtpClient client = new SmtpClient
                {
                    Host = configuration.SmtpHost,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(configuration.SendersEmail, configuration.SendersEmailPassword),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                await client.SendMailAsync(msg).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError($"SendNotification ::: SendEmail :: Exception : {ex.Message}");
            }
        }

        public async void SendEmail(MailConfiguration mailConfiguration)
        {
            var email = new MailMessage();
            email.To.Add(mailConfiguration.ReceiversEmail);
            email.From = new MailAddress("hello@flyanra.com");
            if (!string.IsNullOrEmpty(mailConfiguration.CC))
            {
                email.CC.Add(mailConfiguration.CC);
            }

            email.Subject = mailConfiguration.Subject;
            email.Body = mailConfiguration.Body;
            email.IsBodyHtml = true;

            try
            {
                SmtpClient client = new SmtpClient();
                client.Host = "mail.flyanra.com";
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("hello@flyanra.com", "I_Eew6T9]QlF");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                await client.SendMailAsync(email).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
