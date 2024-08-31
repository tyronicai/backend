namespace OAK.Model.ApiModels.RequestMdl
{
    public class ResetPasswordReqMdl
    {
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string token { get; set; }
    }
}