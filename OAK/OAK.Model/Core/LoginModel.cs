namespace OAK.Model.Core
{
    using System.ComponentModel.DataAnnotations;
    public class LoginModel
    {
        [Required(ErrorMessage = "LoginModel.Email.Required")]
        [MaxLength(100, ErrorMessage = "LoginModel.Email.MaxLength")]
        public string Email { get; set; }


        [Required(ErrorMessage = "LoginModel.Password.Required")]
        public string Password { get; set; }
    }
}
