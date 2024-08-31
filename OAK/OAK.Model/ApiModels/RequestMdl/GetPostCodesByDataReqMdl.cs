using OAK.Model.RequestModels;

namespace OAK.Model.ApiModels.RequestMdl
{
    public class GetPostCodesByDataReqMdl
    {
        public RequestBaseModel RequestBaseMdl { get; set; }
        public int countryId { get; set; }
        public string postCodeStr { get; set; }
        public string placeNameStr { get; set; }
    }
}
