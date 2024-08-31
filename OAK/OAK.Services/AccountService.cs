using AutoMapper;
using OAK.Model.ApiModels.RequestMdl;
using System.Globalization;
using System.IO;

namespace OAK.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;
    using Microsoft.Extensions.Options;
    using MimeKit;
    using OAK.Data;
    using OAK.Data.Paging;
    using OAK.Model.ConfigurationModels;
    using OAK.Model.Core;
    using OAK.Model.ResultModels;
    using OAK.Model.ResultModels.AccountModels;
    using OAK.ServiceContracts;
    using OAK.Validation.AccountValidation;
    using OAK.Validation.AccountValidation.Interfaces;
    using OAK.Validation.TokenValidation;
    using OAK.Validation.TokenValidation.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    public class AccountService : IAccountService
    {
        public IUnitOfWork UnitOfWork { get; }
        public AccountSettings AccountSettings { get; }
        public DocumentSettings DocumentSettings { get; }
        public IEmailService EmailService { get; }
        public IAccountValidator AccountValidator { get; }
        public ITokenValidator TokenValidator { get; }
        public IAuthenticationProviderService TokenService { get; }
        public IPasswordHasherService PasswordHasherService { get; }
        public IRoleService RoleService { get; }
        private readonly IMapper _mapper;


        public AccountService(IUnitOfWork unitOfWork, AccountSettings accountSettings)
        {
            UnitOfWork = unitOfWork;
            AccountSettings = accountSettings;
            AccountValidator = new AccountValidation();
            TokenValidator = new TokenValidation();
        }

        public AccountService(
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            IOptions<AccountSettings> accountSettings,
            IOptions<DocumentSettings> documentSettings,
            IAccountValidator accountValidator,
            ITokenValidator tokenValidator,
            IAuthenticationProviderService tokenService,
            IPasswordHasherService passwordHasherService,
            IRoleService roleService,
            IMapper mapper
        )
        {
            UnitOfWork = unitOfWork;
            AccountSettings = accountSettings.Value;
            DocumentSettings = documentSettings.Value;
            EmailService = emailService;
            AccountValidator = accountValidator;
            TokenValidator = tokenValidator;
            TokenService = tokenService;
            PasswordHasherService = passwordHasherService;
            RoleService = roleService;
            _mapper = mapper;
        }

        public LoginResultModel Authenticate(LoginModel loginModel, IDbContextTransaction trans = null)
        {
            LoginResultModel loginValidationModel = AccountValidator.LoginValidation(loginModel);
            var repo = UnitOfWork.GetRepository<Account>();
            Account account = null;
            bool localTrans = false;
            bool errorOccured = false;

            if (loginValidationModel.IsValid)
            {
                localTrans = null == trans;
                trans = UnitOfWork.BeginTransaction(trans);

                try
                {
                    account = repo.Single(
                        predicate: x => x.Email == loginModel.Email,
                        include: source => source.Include(x => x.AccountRoles).ThenInclude(accRoles => accRoles.Role),
                        disableTracking: true
                    );

                    if (account == null)
                    {
                        loginValidationModel.IsValid = false;
                        loginValidationModel.Description = "account null";
                        loginValidationModel.StatusCode = 11;
                        errorOccured = true;
                    }
                    else
                    {
                        PasswordVerificationResult passwordVerificationResult =
                            PasswordHasherService.VerifiyAccountPassword(account, account.Password, loginModel.Password);

                        if (passwordVerificationResult == PasswordVerificationResult.Failed)
                        {
                            account.LoginAttempts++;
                            repo.Update(account);
                            UnitOfWork.SaveChanges();
                            loginValidationModel.IsValid = false;
                            loginValidationModel.Description = "password incorrect.";
                            loginValidationModel.StatusCode = 12;

                        }
                        else
                        {
                            account.LastLoginDate = DateTime.Now;
                            repo.Update(account);
                            UnitOfWork.SaveChanges();
                            loginValidationModel.Account = account;
                            loginValidationModel.Token = TokenService.GetToken(account);
                            loginValidationModel.RefreshToken = TokenService.GetRefresh(account);
                        }
                    }

                }
                catch (Exception e)
                {
                    loginValidationModel.IsValid = false;
                    errorOccured = true;
                }

                if (!errorOccured)
                {
                    if (localTrans)
                    {
                        UnitOfWork.CommitTransaction(trans);
                    }
                }
                else
                {
                    if (localTrans)
                    {
                        UnitOfWork.RollbackTransaction(trans);
                    }
                }

            }

            return loginValidationModel;
        }

        public Account MakeCompanyOwner(int accountId, IDbContextTransaction trans = null)
        {
            var repo = UnitOfWork.GetRepository<Account>();
            Account acc = null;
            bool localTrans = false;
            bool errorOccured = false;
            if (accountId != null)
            {
                localTrans = null == trans;
                trans = UnitOfWork.BeginTransaction(trans);

                try
                {
                    acc = repo.Single(predicate: x => x.Id == accountId);
                    acc.IsCompanyOwner = true;
                    acc.Modified = DateTime.Now;
                    repo.Update(acc);
                    UnitOfWork.SaveChanges();
                }
                catch (Exception ex)
                {
                    errorOccured = true;
                }

                if (!errorOccured)
                {
                    if (localTrans)
                    {
                        UnitOfWork.CommitTransaction(trans);
                    }
                }
                else
                {
                    if (localTrans)
                    {
                        UnitOfWork.RollbackTransaction(trans);
                    }
                }
            }
            return acc;
        }

        public TokenResultModel RefreshToken(TokenModel tokenModel, IDbContextTransaction trans = null)
        {
            TokenResultModel tokenResultModel = TokenValidator.JwtTokenValidation(tokenModel);
            var repo = UnitOfWork.GetRepository<Account>();
            Account account = null;
            bool localTrans = false;
            bool errorOccured = false;

            if (tokenResultModel.IsValid)
            {
                localTrans = null == trans;
                trans = UnitOfWork.BeginTransaction(trans);

                try
                {
                    var refreshToken = new JwtSecurityToken(tokenModel.RefreshToken);
                    var username = refreshToken.Claims.First(claim => claim.Type == "unique_name").Value;

                    account = repo.Single(predicate: x => x.Username == username);

                    if (null == account)
                    {
                        tokenResultModel.IsValid = false;
                        tokenResultModel.Description = "account null";
                    }
                    else
                    {
                        tokenResultModel.AccessToken = TokenService.GetToken(account);
                        tokenResultModel.RefreshToken = TokenService.GetRefresh(account);
                    }
                }
                catch (Exception e)
                {
                    errorOccured = true;
                }

                if (!errorOccured)
                {
                    if (localTrans)
                    {
                        UnitOfWork.CommitTransaction(trans);
                    }
                }
                else
                {
                    if (localTrans)
                    {
                        UnitOfWork.RollbackTransaction(trans);
                    }
                }
            }

            return tokenResultModel;
        }

        public VerifyEmailResultModel VerifyEmail(VerifyEmailModel verifyEmailModel, IDbContextTransaction trans = null)
        {
            VerifyEmailResultModel verifyEmailResultModel = AccountValidator.EmailValidation(verifyEmailModel);

            var repo = UnitOfWork.GetRepository<Account>();
            Account account = null;
            bool localTrans = false;
            bool errorOccured = false;

            if (verifyEmailResultModel.IsValid)
            {
                localTrans = null == trans;
                trans = UnitOfWork.BeginTransaction(trans);

                try
                {
                    account = repo.Single(predicate: x => x.Email == verifyEmailModel.Email);

                    if (null == account)
                    {
                        verifyEmailResultModel.IsValid = false;
                        verifyEmailResultModel.Description = "User not found!";
                        errorOccured = true;
                    }
                    else
                    {
                        if (account.IsEmailActivated)
                        {
                            verifyEmailResultModel.IsValid = false;
                            verifyEmailResultModel.Description = "Email already approved";
                            errorOccured = true;
                        }
                        else
                        {
                            if (account.ActivationCode != verifyEmailModel.ActivationCode)
                            {
                                verifyEmailResultModel.IsValid = false;
                                verifyEmailResultModel.Description = "Activation code not correct.";
                                errorOccured = true;
                            }
                            else
                            {
                                account.EmailActivationDate = DateTime.Now;
                                account.IsEmailActivated = true;
                                repo.Update(account);
                                UnitOfWork.SaveChanges();

                                MimeMessage mail = new MimeMessage();
                                mail.To.Add(new MailboxAddress(account.Email, ""));


                                var pathToFile = DocumentSettings.HtmlFilesDocumentPath +
                                                 "EmailConfirmation_" +
                                                 CultureInfo.CurrentCulture.Name +
                                                 ".html";
                                string htmlFileContent;
                                using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                                {

                                    htmlFileContent = SourceReader.ReadToEnd();

                                }
                                string htmlString = string.Format(@htmlFileContent, account.FirstName);
                                mail.Subject = getTitle(htmlString);
                                mail.Body = new TextPart("html")
                                {
                                    Text = htmlString
                                };

                                EmailService.Send(mail);

                                verifyEmailResultModel.IsValid = true;
                                verifyEmailResultModel.Description = "Email activated!";
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    errorOccured = true;
                }

                if (!errorOccured)
                {
                    if (localTrans)
                    {
                        UnitOfWork.CommitTransaction(trans);
                    }
                }
                else
                {
                    if (localTrans)
                    {
                        UnitOfWork.RollbackTransaction(trans);
                    }
                }

            }
            return verifyEmailResultModel;
        }

        public VerifyEmailByIdResultModel VerifyEmailById(string transactionId, IDbContextTransaction trans = null)
        {
            //LSG_Control. Aşağıdaki satır?
            VerifyEmailByIdResultModel emailByIdResultModel = AccountValidator.EmailValidationById(transactionId);
            var repo = UnitOfWork.GetRepository<Account>();
            Account account = null;
            bool localTrans = false;
            bool errorOccured = false;

            if (emailByIdResultModel.IsValid)
            {
                localTrans = null == trans;
                trans = UnitOfWork.BeginTransaction(trans);
                try
                {
                    account = repo.Single(x => x.TempVerificationString == transactionId);
                    if (null == account)
                    {
                        emailByIdResultModel.IsValid = false;
                        emailByIdResultModel.Description = "User not found!";
                        errorOccured = true;
                    }
                    else
                    {
                        account.EmailActivationDate = DateTime.Now;
                        account.IsEmailActivated = true;
                        account.TempVerificationString = "";
                        repo.Update(account);
                        UnitOfWork.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    errorOccured = true;
                }

                if (!errorOccured)
                {
                    if (localTrans)
                    {
                        UnitOfWork.CommitTransaction(trans);
                    }
                }
                else
                {
                    if (localTrans)
                    {
                        UnitOfWork.RollbackTransaction(trans);
                    }
                }
            }
            return emailByIdResultModel;
        }

        public IPaginate<Account> GetAll()
        {
            IPaginate<Account> accounts = UnitOfWork.GetReadOnlyRepository<Account>().GetList();
            return accounts;
        }

        public Account GetById(int id)
        {
            Account account = UnitOfWork.GetRepository<Account>().Single(x => x.Id == id);

            return account;
        }

        public Account ChangePassword(ChangePasswordModel changePasswordModel, IDbContextTransaction trans = null)
        {
            var repo = UnitOfWork.GetRepository<Account>();
            Account _account = null;
            bool localTrans = false;
            bool errorOccured = false;
            try
            {
                localTrans = null == trans;
                trans = UnitOfWork.BeginTransaction(trans);

                _account = repo.Single(x => x.Id == changePasswordModel.Account.Id);
                var isPasswordValid = PasswordHasherService.VerifiyAccountPassword(_account, _account.Password, changePasswordModel.Account.Password);
                if (isPasswordValid == PasswordVerificationResult.Failed)
                {
                    _account = null;
                }
                else
                {
                    string password = PasswordHasherService.HashAccountPassword(changePasswordModel.Account, changePasswordModel.NewPassword);
                    _account.Password = password;
                    repo.Update(_account);
                    UnitOfWork.SaveChanges();
                }

            }
            catch (Exception e)
            {
                errorOccured = true;
            }
            if (!errorOccured)
            {
                if (localTrans)
                {
                    UnitOfWork.CommitTransaction(trans);
                }
            }
            else
            {
                if (localTrans)
                {
                    UnitOfWork.RollbackTransaction(trans);
                }
            }
            return _account;
        }

        public RegisterResultModel Register(Account account, IDbContextTransaction trans = null)
        {
            RegisterResultModel registerValidationModel = AccountValidator.RegisterValidation(account);
            var repo = UnitOfWork.GetRepository<Account>();
            Account existUser;
            int activationCode = (new Random()).Next(100000, 999999);
            string userGuid = Guid.NewGuid().ToString();

            bool localTrans = false;
            bool errorOccured = false;
            if (registerValidationModel.IsValid)
            {
                localTrans = null == trans;
                trans = UnitOfWork.BeginTransaction(trans);

                try
                {
                    existUser = repo.Single(predicate: x => x.Email == account.Email);

                    if (null != existUser)
                    {
                        registerValidationModel.Description = "Email is in use.";
                        registerValidationModel.IsValid = false;
                        registerValidationModel.StatusCode = 21;
                        errorOccured = true;
                    }
                    else
                    {
                        existUser = repo.Single(predicate: x => x.Username == account.Username);

                        if (null != existUser)
                        {
                            registerValidationModel.Description = "Username is in use.";
                            registerValidationModel.IsValid = false;
                            registerValidationModel.StatusCode = 22;
                            errorOccured = true;
                        }
                        else
                        {
                            string password = PasswordHasherService.HashAccountPassword(account, account.Password);

                            account.Password = password;

                            account.LastPasswordChangeDate = DateTime.Now;
                            account.IsEmailActivated = false;

                            AccountRole accountRole = new AccountRole();

                            var defaultRole = RoleService.GetDefault(true, trans);
                            accountRole.RoleId = defaultRole.Id;

                            account.AccountRoles = new List<AccountRole>();
                            account.AccountRoles.Add(accountRole);

                            account.TempVerificationString = userGuid;
                            account.FcmToken = userGuid;



                            account.ActivationCode = activationCode;
                            registerValidationModel.UserId = userGuid;
                            UnitOfWork.GetRepository<Account>().Add(account);
                            UnitOfWork.SaveChanges();

                        }
                    }
                }
                catch (Exception ex)
                {
                    errorOccured = true;
                }

                if (!errorOccured)
                {
                    if (localTrans)
                    {
                        UnitOfWork.CommitTransaction(trans);
                    }
                    sendActivationMail(account, userGuid, activationCode.ToString());
                }
                else
                {
                    if (localTrans)
                    {
                        UnitOfWork.RollbackTransaction(trans);
                    }
                }
            }

            return registerValidationModel;
        }

        public ResultBaseModel AddRoleToAccount(AccountRole accountRole, IDbContextTransaction trans = null)
        {
            ResultBaseModel resultBaseModel = new ResultBaseModel();
            bool localTrans = null == trans;
            bool errorOccured = false;
            trans = UnitOfWork.BeginTransaction(trans);

            try
            {
                UnitOfWork.GetRepository<AccountRole>().Add(accountRole);
                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                errorOccured = true;
            }

            if (!errorOccured)
            {
                if (localTrans)
                {
                    UnitOfWork.CommitTransaction(trans);

                }
            }
            else
            {
                if (localTrans)
                {
                    UnitOfWork.RollbackTransaction(trans);
                }
            }

            return resultBaseModel;
        }

        public Account GetByUserName(string userName)
        {
            return UnitOfWork.GetRepository<Account>().Single(x => x.Username == userName);
        }

        public bool IsAccountAvailableByUserName(string userName)
        {
            return (null != UnitOfWork.GetRepository<Account>().Single(x => x.Username == userName));
        }

        public Account UpdateAccount(Account account, IDbContextTransaction trans = null)
        {
            var repo = UnitOfWork.GetRepository<Account>();
            Account acc;
            bool localTrans = false;
            bool errorOccured = false;
            if (account != null)
            {
                localTrans = null == trans;
                trans = UnitOfWork.BeginTransaction(trans);

                try
                {
                    acc = repo.Single(predicate: x => x.Email == account.Email);
                    acc = _mapper.Map<Account>(account);
                    acc.Modified = DateTime.Now;
                    account = acc;
                    repo.Update(acc);
                    UnitOfWork.SaveChanges();

                }
                catch (Exception ex)
                {
                    errorOccured = true;
                }

                if (!errorOccured)
                {
                    if (localTrans)
                    {
                        UnitOfWork.CommitTransaction(trans);
                    }
                }
                else
                {
                    if (localTrans)
                    {
                        UnitOfWork.RollbackTransaction(trans);
                    }
                }
            }
            return account;
        }

        public Account ResetPassword(ResetPasswordReqMdl resetPasswordReqMdl, IDbContextTransaction trans = null)
        {
            var repo = UnitOfWork.GetRepository<Account>();
            Account acc = repo.Single(a => a.TempVerificationString == resetPasswordReqMdl.token);
            bool localTrans = false;
            bool errorOccured = false;
            if (acc != null)
            {
                localTrans = null == trans;
                trans = UnitOfWork.BeginTransaction(trans);
                try
                {
                    acc = repo.Single(predicate: x => x.TempVerificationString == resetPasswordReqMdl.token);
                    acc.Password = PasswordHasherService.HashAccountPassword(acc, resetPasswordReqMdl.password);
                    acc.TempVerificationString = "";
                    acc.Modified = DateTime.Now;
                    repo.Update(acc);
                    UnitOfWork.SaveChanges();
                }
                catch (Exception ex)
                {
                    errorOccured = true;
                }

                if (!errorOccured)
                {
                    if (localTrans)
                    {
                        UnitOfWork.CommitTransaction(trans);
                    }
                }
                else
                {
                    if (localTrans)
                    {
                        UnitOfWork.RollbackTransaction(trans);
                    }
                }
            }
            return acc;
        }


        public ForgotPasswordResultModel SendForgotPasswordEmail(ForgotPasswordModel forgotPasswordModel, IDbContextTransaction trans = null)
        {
            ForgotPasswordResultModel forgotPasswordResultModel = new ForgotPasswordResultModel();
            var repo = UnitOfWork.GetRepository<Account>();
            Account account = null;
            bool localTrans = false;
            bool errorOccured = false;

            if (forgotPasswordResultModel.IsValid)
            {
                localTrans = null == trans;
                trans = UnitOfWork.BeginTransaction(trans);
                try
                {
                    account = repo.Single(x => x.Email == forgotPasswordModel.Email);
                    if (null == account)
                    {
                        forgotPasswordResultModel.IsValid = false;
                        forgotPasswordResultModel.Description = "Email not found!";
                        errorOccured = true;
                    }
                    else
                    {

                        string userGuid = Guid.NewGuid().ToString();
                        account.TempVerificationString = userGuid;
                        UnitOfWork.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    errorOccured = true;
                }

                if (!errorOccured)
                {
                    if (localTrans)
                    {
                        UnitOfWork.CommitTransaction(trans);
                    }

                    var pathToFile = DocumentSettings.HtmlFilesDocumentPath +
                                     "ForgotPassword_" +
                                     CultureInfo.CurrentCulture.Name +
                                     ".html";
                    string htmlFileContent;
                    using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                    {
                        htmlFileContent = SourceReader.ReadToEnd();
                    }

                    MimeMessage mail = new MimeMessage();
                    //string forgotpasswordUrl = AccountSettings.Url + "/api/tr-TR/Accounts/forgotPassword/" + account.TempVerificationString;
                    string resetpasswordUrl = AccountSettings.FrontendUrl +
                                              "/login/resetPassword?token=" + account.TempVerificationString +
                        "&culture=" + CultureInfo.CurrentCulture.Name;
                    string htmlString = string.Format(htmlFileContent, account.FirstName, resetpasswordUrl);
                    mail.To.Add(new MailboxAddress(account.Email, ""));
                    mail.Subject = getTitle(htmlString);
                    mail.Body = new TextPart("html")
                    {
                        Text = htmlString
                    };
                    EmailService.Send(mail);
                }
                else
                {
                    if (localTrans)
                    {
                        UnitOfWork.RollbackTransaction(trans);
                    }
                }
            }
            return forgotPasswordResultModel;
        }

        public bool ResendActivationEmail(Account account, IDbContextTransaction trans = null)
        {
            var repo = UnitOfWork.GetRepository<Account>();
            bool localTrans = false;
            bool errorOccured = false;
            int activationCode = (new Random()).Next(100000, 999999);
            string userGuid = Guid.NewGuid().ToString();

            var acc = repo.Single(a => a.Email == account.Email);
            if (acc == null)
            {
                return false;
            }

            localTrans = null == trans;
            trans = UnitOfWork.BeginTransaction(trans);
            try
            {
                acc.ActivationCode = activationCode;
                acc.TempVerificationString = userGuid;
                UnitOfWork.SaveChanges();
            }
            catch (Exception e)
            {
                errorOccured = true;
            }

            if (!errorOccured)
            {
                if (localTrans)
                {
                    UnitOfWork.CommitTransaction(trans);
                }

                sendActivationMail(account, userGuid, activationCode.ToString());
            }
            else
            {
                if (localTrans)
                {
                    UnitOfWork.RollbackTransaction(trans);
                }
            }
            return true;
        }

        private string getTitle(string inputString)
        {
            int pFrom = inputString.IndexOf("<title>") + "<title>".Length;
            int pTo = inputString.IndexOf("</title>");

            String result = inputString.Substring(pFrom, pTo - pFrom);
            return result;
        }

        private void sendActivationMail(Account account, string userGuid, string activationCode)
        {
            var pathToFile = DocumentSettings.HtmlFilesDocumentPath +
                             "AccountVerification_" +
                             CultureInfo.CurrentCulture.Name +
                             ".html";
            string htmlFileContent;
            try
            {
                using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                {
                    htmlFileContent = SourceReader.ReadToEnd();
                }

                string validationUrl = AccountSettings.Url + "/api/tr-TR/Accounts/verifyEmailById/" + userGuid;
                string htmlString = string.Format(htmlFileContent, account.FirstName, activationCode, validationUrl);

                MimeMessage mail = new MimeMessage();
                mail.To.Add(new MailboxAddress(account.Email, ""));
                mail.Subject = getTitle(htmlString);
                mail.Body = new TextPart("html")
                {
                    Text = htmlString
                };


                EmailService.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}