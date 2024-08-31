namespace OAK.Model.ResultModels.AccountModels
{
    public class RegisterResultModel : ResultBaseModel
    {
        public string UserId { get; set; }

        public RegisterResultModel()
        {

        }

        public RegisterResultModel(bool isValid, string description, int statusCode) : base(isValid, description, statusCode)
        {

        }
    }
}
