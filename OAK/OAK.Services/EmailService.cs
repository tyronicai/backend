namespace OAK.Services
{
    using Microsoft.Extensions.Options;
    using MimeKit;
    using Model.ConfigurationModels;
    using SendGrid;
    using SendGrid.Helpers.Mail;
    using ServiceContracts;
    using System;

    public class EmailService : IEmailService
    {
        public SmtpSettings SmtpSettings { get; }
        private string apiKey { get; set; }

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            SmtpSettings = smtpSettings?.Value ?? throw new ArgumentException("smtpSettings");
            apiKey = "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";
        }

        public void SendPrev(MimeMessage mailMessage)
        {
            mailMessage.From.Add(new MailboxAddress(SmtpSettings.NetworkCredential.UserName, SmtpSettings.NetworkCredential.UserName));

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {

                    client.Connect(SmtpSettings.Host, SmtpSettings.Port, SmtpSettings.EnableSsl);

                    //SMTP server authentication if needed
                    client.Authenticate(SmtpSettings.NetworkCredential.UserName, SmtpSettings.NetworkCredential.Password);

                    client.Send(mailMessage);

                    client.Disconnect(true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void Send(MimeMessage mailMessage)
        {
            mailMessage.From.Add(new MailboxAddress(SmtpSettings.NetworkCredential.UserName, SmtpSettings.NetworkCredential.UserName));

            //var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("leventsezgin.genc@tyronicai.com", "Info User");
            var subject = mailMessage.Subject;
            var to = new EmailAddress(mailMessage.To.ToString(), "");
            var plainTextContent = "";
            var htmlContent = mailMessage.HtmlBody;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }
    }
}
