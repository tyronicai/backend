namespace OAK.Model.ResultModels.AccountModels
{
    public class VerifyEmailByIdResultModel : ResultBaseModel
    {
        public VerifyEmailByIdResultModel()
        {

        }

        public VerifyEmailByIdResultModel(bool isValid, string description, int statusCode) : base(isValid, description, statusCode)
        {

        }

    }
}
