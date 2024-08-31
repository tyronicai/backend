using OAK.Model.BusinessModels.AddressModels;
using OAK.Model.BusinessModels.CompanyModels;
using OAK.Model.Core;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class GetCompanyByOwnerResMdl
    {
        public Company Company { get; set; }
        public GenericAddress GenericAddress { get; set; }
        public Country Country { get; set; }
        public CompanyStatusType CompanyStatusType { get; set; }
    }
}