using OAK.Model.Core;
using OAK.Model.ResultModels;

namespace OAK.Model.ApiModels.RequestMdl
{
    public class CreateCompanyPostCodeDateResMdl
    {
        public CreateCompanyPostCodeDateResMdl()
        {
            ResultBaseMdl = new ResultBaseModel();
        }

        public ResultBaseModel ResultBaseMdl { get; set; }
        public CompanyPostCodeData CompanyPostCodeData { get; set; }
    }
}