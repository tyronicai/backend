namespace OAK.Model.ConfigurationModels
{
    using System.Net;
    using System.Net.Mail;

    public class SmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }

        public bool EnableSsl { get; set; }

        public SmtpDeliveryMethod SmtpDeliveryMethod { get; set; }
        public string PickupDirectoryLocation { get; set; }

        public bool UseDefaultCredentials { get; set; }

        public NetworkCredential NetworkCredential { get; set; }
    }
}
