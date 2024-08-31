using Microsoft.AspNetCore.Identity;

namespace OAK.Model.ConfigurationModels
{
    public class AccountSettings
    {
        public PasswordHasherOptions PasswordHasherOptions { get; set; }
        public int MaxLoginAttempts { get; set; }
        public string Url { get; set; }
        public string FrontendUrl { get; set; }
    }
}
