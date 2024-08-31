namespace OAK.Model.ResultModels.AccountModels
{
    public class ForgotPasswordResultModel : ResultBaseModel
    {
        public ForgotPasswordResultModel()
        {

        }

        public ForgotPasswordResultModel(bool isValid, string description, int statusCode) : base(isValid, description, statusCode)
        {

        }

    }
}