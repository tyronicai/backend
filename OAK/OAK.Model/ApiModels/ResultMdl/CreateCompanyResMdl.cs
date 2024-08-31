using OAK.Model.BusinessModels.CompanyModels;
using OAK.Model.ResultModels;

namespace OAK.Model.ApiModels.RequestMdl
{
    public class CreateCompanyResMdl
    {
        public CreateCompanyResMdl()
        {
            ResultBaseMdl = new ResultBaseModel();
        }

        public ResultBaseModel ResultBaseMdl { get; set; }
        public Company Company { get; set; }
    }
}