
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.DemandModels;
using OAK.Model.RequestModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.RequestMdl
{
    public class DemandStatusTypeReqMdl
    {
        public DemandStatusTypeReqMdl()
        {
            LanguageIdTexts = new List<LanguageIdText>();
        }
        public RequestBaseModel RequestBaseMdl { get; set; }
        public DemandStatusType DemandStatusType { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
