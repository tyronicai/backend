using OAK.Model.BusinessModels.AddressModels;
using OAK.Model.BusinessModels.DemandModels;
using OAK.Model.BusinessModels.EstateModels;
using OAK.Model.Core;

namespace OAK.WebReport.Services.ServiceContracts
{
    using OAK.Data;
    using OAK.Data.Paging;

    public interface IReportService
    {
        IPaginate<EstateDetailView> GetEstateDetailsAndFurnitures(int id);

        Account GetAccount(int estateId);

        DemandView GetDemandDetails(int demandId);

        GenericAddress GetEstateAddress(int estateId);

        string GetFurnitureName(string localKey, string cultureName);
    }
}