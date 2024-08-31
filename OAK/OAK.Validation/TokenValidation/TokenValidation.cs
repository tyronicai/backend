using System;

namespace OAK.Validation.TokenValidation
{
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using OAK.Model.ConfigurationModels;
    using OAK.Model.Core;
    using OAK.Model.ResultModels.AccountModels;
    using OAK.Validation.TokenValidation.Interfaces;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;

    public class TokenValidation : ITokenValidator
    {
        public TokenSettings TokenSettings { get; }

        public TokenValidation(IOptions<TokenSettings> options)
        {
            TokenSettings = options.Value;
        }

        public TokenValidation() { }

        public TokenValidation(TokenSettings tokenSettings)
        {
            TokenSettings = tokenSettings;
        }
        public TokenResultModel JwtTokenValidation(TokenModel tokenModel)
        {
            TokenResultModel tokenResultModel = new TokenResultModel();
            string accessToken = tokenModel.AccessToken;
            string refreshToken = tokenModel.RefreshToken;

            // TODO RefreshToken'ın expire süresine daha çok varsa var olan RefreshToken geri döndürülecek. 

            var validationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenSettings.ClaimSecret)),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            try
            {
                handler.ValidateToken(accessToken, validationParameters, out var validAccessToken);
                JwtSecurityToken validAccessJwt = validAccessToken as JwtSecurityToken;
            }
            catch (Exception e)
            {
                tokenResultModel.IsValid = false;
                return tokenResultModel;
            }

            try
            {
                handler.ValidateToken(refreshToken, validationParameters, out var validRefreshToken);
                JwtSecurityToken validRefreshJwt = validRefreshToken as JwtSecurityToken;

            }
            catch (Exception e)
            {
                Exception x = e;
                tokenResultModel.IsValid = false;
                return tokenResultModel;
            }

            tokenResultModel.IsValid = true;
            return tokenResultModel;
        }
    }
}
