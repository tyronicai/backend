namespace OAK.Services
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using OAK.Model.ConfigurationModels;
    using OAK.Model.Core;
    using OAK.ServiceContracts;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class AuthenticationProviderService : IAuthenticationProviderService
    {
        public TokenSettings TokenSettings { get; }

        public AuthenticationProviderService(IOptions<TokenSettings> options)
        {
            TokenSettings = options.Value;
        }

        public AuthenticationProviderService(TokenSettings tokenSettings)
        {
            TokenSettings = tokenSettings;
        }

        public string GetToken(Account account)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(TokenSettings.ClaimSecret);

            Claim[] claims = new Claim[2];
            claims[0] = new Claim(ClaimTypes.NameIdentifier, account.Id.ToString());
            claims[1] = new Claim(ClaimTypes.Name, account.Username);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GetRefresh(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(TokenSettings.ClaimSecret);

            Claim[] claims1 = new Claim[3];
            claims1[0] = new Claim(ClaimTypes.NameIdentifier, account.Id.ToString());
            claims1[1] = new Claim(ClaimTypes.Name, account.Username);
            claims1[2] = new Claim(ClaimTypes.Role, "DefaultRole");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims1),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public void ConfigureAuthentication(IServiceCollection serviceCollection)
        {
            var tokenKey = Encoding.ASCII.GetBytes(TokenSettings.ClaimSecret);
            serviceCollection.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
