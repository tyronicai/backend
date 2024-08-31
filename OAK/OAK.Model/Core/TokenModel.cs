using System.ComponentModel.DataAnnotations;

namespace OAK.Model.Core
{
    public class TokenModel
    {
        [Required(ErrorMessage = "TokenModel.AccessToken.Required")]
        public string AccessToken { get; set; }


        [Required(ErrorMessage = "TokenModel.RefreshToken.Required")]
        public string RefreshToken { get; set; }
    }
}