using System.ComponentModel.DataAnnotations;

namespace OAK.Model.Core
{
    public class ForgotPasswordModel
    {

        [Required(ErrorMessage = "Account.Email.Required")]
        [MaxLength(255, ErrorMessage = "ForgotPasswordModel.Email.MaxLength")]
        [EmailAddress(ErrorMessage = "ForgotPasswordModel.Email.EmailAddress")]
        public string Email { get; set; }
    }
}