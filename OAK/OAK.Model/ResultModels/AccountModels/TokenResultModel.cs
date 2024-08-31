namespace OAK.Model.ResultModels.AccountModels
{
    public class TokenResultModel : ResultBaseModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public TokenResultModel()
        {

        }

        public TokenResultModel(bool isValid, string description, int statusCode) : base(isValid, description, statusCode)
        {

        }
    }
}