using OAK.Model.ApiModels.RequestMdl;
using OAK.Validation.CompanyValidation.Interfaces;

namespace OAK.Validation.CompanyValidation
{
    public class CompanyValidation : ICompanyValidator
    {
        public CreateCompanyResMdl CreateCompanyValidation(CreateCompanyReqMdl createCompanyReqMdl)
        {
            CreateCompanyResMdl createCompanyResMdl = new CreateCompanyResMdl();
            createCompanyResMdl.ResultBaseMdl.IsValid = true;
            return createCompanyResMdl;
        }

        public CreateCompanyPostCodeDateResMdl CreateCompanyPostCodeDataValidation(
            CreateCompanyPostCodeDataReqMdl companyPostCodeDataReqMdl)
        {
            CreateCompanyPostCodeDateResMdl createCompanyPostCodeDateResMdl = new CreateCompanyPostCodeDateResMdl();
            createCompanyPostCodeDateResMdl.ResultBaseMdl.IsValid = true;
            return createCompanyPostCodeDateResMdl;
        }
    }
}