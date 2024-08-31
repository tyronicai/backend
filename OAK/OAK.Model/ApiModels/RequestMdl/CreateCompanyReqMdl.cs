using OAK.Model.BusinessModels.AddressModels;
using OAK.Model.BusinessModels.CompanyModels;
using OAK.Model.RequestModels;
using System.ComponentModel.DataAnnotations;

namespace OAK.Model.ApiModels.RequestMdl
{
    public class CreateCompanyReqMdl
    {
        public CreateCompanyReqMdl()
        {
            RequestBaseMdl = new RequestBaseModel();
            Company = new Company();
        }
        [Required(ErrorMessage = "CreateCompanyReqMdl.Company.Required")]
        public Company Company { get; set; }

        public GenericAddress GenericAddress { get; set; }
        public RequestBaseModel RequestBaseMdl { get; set; }
    }
}