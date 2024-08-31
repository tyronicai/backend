namespace OAK.Validation.AccountValidation.Interfaces
{
    using OAK.Model.Core;
    using OAK.Model.ResultModels.AccountModels;

    public interface IAccountValidator
    {
        RegisterResultModel RegisterValidation(Account account);
        LoginResultModel LoginValidation(LoginModel loginModel);
        VerifyEmailResultModel EmailValidation(VerifyEmailModel emailModel);
        VerifyEmailByIdResultModel EmailValidationById(string userId);
    }

    public interface IAccountValidatorTransient : IAccountValidator
    {

    }
}