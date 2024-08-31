namespace OAK.Model.ResultModels
{
    public class ResultBaseModel
    {
        public bool IsValid { get; set; }
        public string Description { get; set; }

        public int StatusCode { get; set; }

        public ResultBaseModel()
        {
            IsValid = true;
            StatusCode = 0;
        }

        public ResultBaseModel(bool _isValid, string _description, int _statusCode)
        {
            IsValid = _isValid;
            Description = _description;
            StatusCode = _statusCode;
        }
    }
}