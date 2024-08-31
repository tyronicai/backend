using System.ComponentModel.DataAnnotations;

namespace OAK.Model.Core
{

    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "ChangePasswordModel.Account.Required")]
        public Account Account { get; set; }


        [Required(ErrorMessage = "ChangePasswordModel.NewPassword.Required")]
        public string NewPassword { get; set; }
    }
}