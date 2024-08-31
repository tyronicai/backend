using OAK.Model.RequestModels;

namespace OAK.Model.ApiModels.RequestMdl
{
    public class PaginateReqMdl
    {
        public RequestBaseModel RequestBaseMdl { get; set; }
        public int index { get; set; }
        public int size { get; set; }
    }
}