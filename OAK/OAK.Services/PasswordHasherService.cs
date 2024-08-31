namespace OAK.Services
{
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using OAK.Model.ConfigurationModels;
    using OAK.Model.Core;
    using OAK.ServiceContracts;
    using System;
    using System.Text;
    public class PasswordHasherService : IPasswordHasherService
    {
        public HashPasswordSettings HashPasswordSettings { get; }
        public AccountSettings AccountSettings { get; }

        public PasswordHasherService(IOptions<HashPasswordSettings> optionsHashPassword, IOptions<AccountSettings> optionsAccount)
        {
            HashPasswordSettings = optionsHashPassword.Value;
            AccountSettings = optionsAccount.Value;
        }

        public string Create(string value)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                                password: value,
                                salt: Encoding.UTF8.GetBytes(HashPasswordSettings.PasswordSalt),
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 10000,
                                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(valueBytes);
        }

        public bool Validate(string value, string hash)
        {
            string newHashed = Create(value);
            return newHashed == hash;
        }

        public PasswordVerificationResult VerifiyAccountPassword(Account account, string hashedPassword, string providedPassword)
        {
            IPasswordHasher<Account> passwordHasher = new PasswordHasher<Account>();
            return passwordHasher.VerifyHashedPassword(account, hashedPassword, providedPassword);
        }

        public string HashAccountPassword(Account account, string password)
        {
            IPasswordHasher<Account> passwordHasher = new PasswordHasher<Account>();
            return passwordHasher.HashPassword(account, password);
        }
    }
}
