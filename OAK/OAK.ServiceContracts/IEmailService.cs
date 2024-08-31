namespace OAK.ServiceContracts
{
    using MimeKit;
    using Model.ConfigurationModels;
    public interface IEmailService
    {
        SmtpSettings SmtpSettings { get; }
        void Send(MimeMessage mailMessage);
    }

}