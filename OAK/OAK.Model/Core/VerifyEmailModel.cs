using System.ComponentModel.DataAnnotations;

namespace OAK.Model.Core
{
    public class VerifyEmailModel
    {
        [Required(ErrorMessage = "VerifyEmail.Id.Required")]
        public string Email { get; set; }


        [Required(ErrorMessage = "VerifyEmail.ActivationCode.Required")]
        public int ActivationCode { get; set; }
    }
}