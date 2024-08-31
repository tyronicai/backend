using OAK.Model.ResultModels;
using OAK.Model.ViewModels.TransportationModels;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class TransportationStatusTypeResMdl
    {
        public TransportationStatusTypeResMdl()
        {

        }
        public ResultBaseModel ResultBaseMdl { get; set; }
        public UeTransportationStatusType UeTransportationStatusType { get; set; }
    }
}
