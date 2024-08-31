using System.ComponentModel.DataAnnotations;

namespace OAK.Model.ApiModels.RequestMdl
{
    public class CreateCompanyPostCodeDataReqMdl
    {
        public CreateCompanyPostCodeDataReqMdl()
        {
        }

        [Required(ErrorMessage = "CreateCompanyPostCodeDataReqMdl.CompanyId.Required")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "CreateCompanyPostCodeDataReqMdl.CountryId.Required")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "CreateCompanyPostCodeDataReqMdl.PostCode.Required")]
        public string PostCode { get; set; }

    }
}