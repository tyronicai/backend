using OAK.Model.BusinessModels.DemandModels;
using OAK.Model.BusinessModels.TransportationModels;
using System.Threading.Tasks;

namespace OAK.ServiceContracts
{
    public interface IReportService
    {

        #region ReportService

        Task<bool> SendDemandReportToCustomer(Demand demand, Transportation transportation);

        #endregion ReportService

    }


}
