using OAK.Model.BusinessModels.EstateModels;
using OAK.Model.RequestModels;

namespace OAK.Model.ApiModels.RequestMdl
{
    public class EstatePartFurnitureReqMdl
    {
        public RequestBaseModel RequestBaseMdl { get; set; }
        public EstatePartFurniture EstatePartFurniture { get; set; }
    }
}
