namespace OAK.Model.RequestModels
{
    public class RequestBaseModel
    {
        public bool IsValid { get; set; }
        public string Description { get; set; }

        public RequestBaseModel()
        {

        }

        public RequestBaseModel(bool isValid, string description)
        {
            IsValid = isValid;
            Description = description;
        }
    }
}