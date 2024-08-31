namespace OAK.ServiceContracts
{
    using Microsoft.AspNetCore.Identity;
    using OAK.Model.ConfigurationModels;
    using OAK.Model.Core;

    public interface IPasswordHasherService
    {
        HashPasswordSettings HashPasswordSettings { get; }
        string Create(string value);
        bool Validate(string value, string hash);
        PasswordVerificationResult VerifiyAccountPassword(Account account, string hashedPassword, string providedPassword);
        string HashAccountPassword(Account account, string password);
    }

}