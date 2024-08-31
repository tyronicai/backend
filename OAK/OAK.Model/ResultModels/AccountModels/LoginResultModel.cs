namespace OAK.Model.ResultModels.AccountModels
{
    using OAK.Model.Core;


    public class LoginResultModel : ResultBaseModel
    {
        public Account Account { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public LoginResultModel()
        {

        }

        public LoginResultModel(bool isValid, string description, int statusCode) : base(isValid, description, statusCode)
        {

        }
    }
}
