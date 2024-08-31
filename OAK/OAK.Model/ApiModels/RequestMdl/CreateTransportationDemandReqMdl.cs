using OAK.Model.RequestModels;
using OAK.Model.ViewModels.DemandModels;
using OAK.Model.ViewModels.TransportationModels;

namespace OAK.Model.ApiModels.RequestMdl
{
    public class CreateTransportationDemandReqMdl
    {

        public RequestBaseModel RequestBaseMdl { get; set; }
        public UeDemand Demand { get; set; }
        public UeTransportation Transportation { get; set; }
    }
}
