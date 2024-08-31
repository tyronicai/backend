using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Localization;
using OAK.Model.ApiModels.RequestMdl;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace OAK.WebApi.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using OAK.Model.Core;
    using OAK.Model.Localization;
    using OAK.Model.StaticModels;
    using OAK.ServiceContracts;

    [Authorize]
    [ApiController]
    //[ServiceFilter(typeof(LanguageActionFilter))]
    [Route("api/{culture}/Accounts")]

    public class AccountsController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly ILogger Logger;
        private readonly IMapper _mapper;

        private readonly IStringLocalizer<AccountsController> _stringLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public AccountsController(IAccountService accountService, IMapper mapper, ILogger<AccountsController> logger, IStringLocalizer<AccountsController> valuesLocalizerizer, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _accountService = accountService;
            Logger = logger;
            _stringLocalizer = valuesLocalizerizer;
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("VerifyEmailById/{id}")]
        public IActionResult VerifyEmailById(string id)
        {
            var response = _accountService.VerifyEmailById(id);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] Account accountModel)
        {
            var registerResult = _accountService.Register(accountModel);
            return Ok(registerResult);
        }

        [AllowAnonymous]
        [HttpPost("verifyemail")]
        public IActionResult VerifyEmail([FromBody] VerifyEmailModel verifyEmailModel)
        {

            var verifyEmailResult = _accountService.VerifyEmail(verifyEmailModel);
            return Ok(verifyEmailResult);
        }

        [AllowAnonymous]
        [HttpPost("refreshtoken")]
        public IActionResult RefreshToken([FromBody] TokenModel tokenModel)
        {
            var refreshTokenResult = _accountService.RefreshToken(tokenModel);
            return Ok(refreshTokenResult);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginModel loginModel)
        {
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            var culture = rqf.RequestCulture.Culture;
            var myCulture = CultureInfo.CurrentCulture.Name;
            var user = _accountService.Authenticate(loginModel);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("addrole")]
        public IActionResult AddRole([FromBody] AccountRole accountRole)
        {
            var result = _accountService.AddRoleToAccount(accountRole);
            return Ok(result);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var users = _accountService.GetAll();
            return Ok(users);
        }

        [HttpGet("getownaccount")]
        [Authorize]
        public IActionResult GetOwnAccount()
        {

            var userId = UserId;

            var user = _accountService.GetById(userId.Value);
            if (user == null)
                return NotFound();

            return Ok(user);
        }



        [HttpPost("ChangePassword")]
        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        public IActionResult ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var result = _accountService.ChangePassword(changePasswordModel);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        public IActionResult GetById(int id)
        {
            var user = _accountService.GetById(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateAccount")]
        public IActionResult UpdateAccount(Account account)
        {
            var acc = _accountService.UpdateAccount(account);
            return Ok(acc);
        }

        [AllowAnonymous]
        [HttpPost("SendPasswordForgotEmail")]
        public IActionResult SendPasswordForgotEmail([FromBody] ForgotPasswordModel forgotPasswordModel)
        {
            var account = _accountService.SendForgotPasswordEmail(forgotPasswordModel);
            return Ok(account);
        }


        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword([FromBody] ResetPasswordReqMdl resetPasswordReqMdl)
        {
            var account = _accountService.ResetPassword(resetPasswordReqMdl);
            return Ok(account);
        }

        [AllowAnonymous]
        [HttpPost("ResendActivationMail")]
        public IActionResult ResendActivationMail([FromBody] Account account)
        {
            var acc = _accountService.ResendActivationEmail(account);
            return Ok(acc);
        }
    }
}
