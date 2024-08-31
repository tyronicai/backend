

namespace OAK.Validation.AccountValidation
{
    using OAK.Model.Core;
    using OAK.Model.ResultModels.AccountModels;
    using OAK.Validation.AccountValidation.Interfaces;
    public class AccountValidation : IAccountValidator
    {
        public LoginResultModel LoginValidation(LoginModel loginModel)
        {
            //TODO

            LoginResultModel loginValidationModel = new LoginResultModel();

            loginValidationModel.IsValid = true;

            return loginValidationModel;
        }

        public RegisterResultModel RegisterValidation(Account account)
        {
            RegisterResultModel registerValidationModel = new RegisterResultModel();
            registerValidationModel.IsValid = true;

            //TODO

            return registerValidationModel;
        }

        public VerifyEmailResultModel EmailValidation(VerifyEmailModel verifyEmailModel)
        {
            VerifyEmailResultModel emailResultModel = new VerifyEmailResultModel();
            emailResultModel.IsValid = true;
            if (verifyEmailModel.ActivationCode.ToString().Length != 6)
            {
                emailResultModel.IsValid = false;
                emailResultModel.Description = "Activation code invalid";

            }
            return emailResultModel;
        }

        public VerifyEmailByIdResultModel EmailValidationById(string userId)
        {
            VerifyEmailByIdResultModel verifyEmailByIdResultModel = new VerifyEmailByIdResultModel();
            verifyEmailByIdResultModel.IsValid = true;
            return verifyEmailByIdResultModel;
        }
    }
}
