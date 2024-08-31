using Microsoft.EntityFrameworkCore.Storage;
using OAK.Model.ApiModels.RequestMdl;

namespace OAK.ServiceContracts
{
    using OAK.Model.ConfigurationModels;
    using OAK.Model.Core;
    using OAK.Model.ResultModels;
    using OAK.Model.ResultModels.AccountModels;
    using OAK.Validation.AccountValidation.Interfaces;
    using OAK.Data;
    using OAK.Data.Paging;

    public interface IAccountService
    {
        IAuthenticationProviderService TokenService { get; }
        IAccountValidator AccountValidator { get; }
        IUnitOfWork UnitOfWork { get; }
        IEmailService EmailService { get; }
        IPasswordHasherService PasswordHasherService { get; }

        IRoleService RoleService { get; }

        AccountSettings AccountSettings { get; }
        VerifyEmailResultModel VerifyEmail(VerifyEmailModel verifyEmailModel, IDbContextTransaction trans = null);
        Account ResetPassword(ResetPasswordReqMdl resetPasswordReqMdl, IDbContextTransaction trans = null);
        Account MakeCompanyOwner(int accountId, IDbContextTransaction trans = null);
        TokenResultModel RefreshToken(TokenModel tokenModel, IDbContextTransaction trans = null);
        LoginResultModel Authenticate(LoginModel loginModel, IDbContextTransaction trans = null);
        IPaginate<Account> GetAll();
        Account GetById(int id);
        Account GetByUserName(string userName);

        Account ChangePassword(ChangePasswordModel changePasswordModel, IDbContextTransaction trans = null);
        VerifyEmailByIdResultModel VerifyEmailById(string transactionId, IDbContextTransaction trans = null);
        bool IsAccountAvailableByUserName(string userName);

        RegisterResultModel Register(Account account, IDbContextTransaction trans = null);

        ResultBaseModel AddRoleToAccount(AccountRole accountRole, IDbContextTransaction trans = null);

        Account UpdateAccount(Account account, IDbContextTransaction trans = null);
        ForgotPasswordResultModel SendForgotPasswordEmail(ForgotPasswordModel forgotPasswordModel, IDbContextTransaction trans = null);

        bool ResendActivationEmail(Account account, IDbContextTransaction trans = null);

    }

}