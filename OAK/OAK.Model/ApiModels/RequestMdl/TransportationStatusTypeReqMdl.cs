
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.TransportationModels;
using OAK.Model.RequestModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.RequestMdl
{
    public class TransportationStatusTypeReqMdl
    {
        public TransportationStatusTypeReqMdl()
        {
            LanguageIdTexts = new List<LanguageIdText>();
        }
        public RequestBaseModel RequestBaseMdl { get; set; }
        public TransportationStatusType TransportationStatusType { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
