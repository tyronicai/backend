using OAK.Model.ConfigurationModels;

namespace OAK.Validation.TokenValidation.Interfaces
{
    using OAK.Model.Core;
    using OAK.Model.ResultModels.AccountModels;

    public interface ITokenValidator
    {
        TokenResultModel JwtTokenValidation(TokenModel tokenModel);
        TokenSettings TokenSettings { get; }
    }

    public interface ITokenValidatorTransient : ITokenValidator
    {

    }
}