using OAK.Model.RequestModels;
using OAK.Model.ViewModels.TransportationModels;

namespace OAK.Model.ApiModels.RequestMdl
{
    public class TransportationReqMdl
    {
        public RequestBaseModel RequestBaseMdl { get; set; }
        public UeTransportation Transportation { get; set; }
    }
}
