namespace OAK.Model.ResultModels.AccountModels
{
    public class VerifyEmailResultModel : ResultBaseModel
    {
        public VerifyEmailResultModel()
        {

        }

        public VerifyEmailResultModel(bool isValid, string description, int statusCode) : base(isValid, description, statusCode)
        {

        }

    }
}