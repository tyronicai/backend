using OAK.Model.BusinessModels.EstateModels;
using OAK.Model.RequestModels;

namespace OAK.Model.ApiModels.RequestMdl
{
    public class EstateTypeEPartTypeReqMdl
    {
        public RequestBaseModel RequestBaseMdl { get; set; }
        public EstateTypeEPartType EstateTypeEPartType { get; set; }
    }
}
