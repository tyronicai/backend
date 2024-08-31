namespace OAK.Validation.CompanyValidation.Interfaces
{
    using OAK.Model.ApiModels.RequestMdl;

    public interface ICompanyValidator
    {
        CreateCompanyResMdl CreateCompanyValidation(CreateCompanyReqMdl createCompanyReqMdl);
        CreateCompanyPostCodeDateResMdl CreateCompanyPostCodeDataValidation(CreateCompanyPostCodeDataReqMdl companyPostCodeDataReqMdl);
    }

    public interface ICompanyValidatorTransient : ICompanyValidator
    {

    }
}