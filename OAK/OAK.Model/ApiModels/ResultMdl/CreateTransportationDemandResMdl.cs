using OAK.Model.ResultModels;
using OAK.Model.ViewModels.DemandModels;
using OAK.Model.ViewModels.TransportationModels;

namespace OAK.Model.ApiModels.RequestMdl
{
    public class CreateTransportationDemandResMdl
    {

        public ResultBaseModel ResultBaseMdl { get; set; }
        public UeDemand Demand { get; set; }
        public UeTransportation Transportation { get; set; }
    }
}
