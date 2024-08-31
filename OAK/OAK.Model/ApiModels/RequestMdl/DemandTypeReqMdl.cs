
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.DemandModels;
using OAK.Model.RequestModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.RequestMdl
{
    public class DemandTypeReqMdl
    {
        public DemandTypeReqMdl()
        {
            LanguageIdTexts = new List<LanguageIdText>();
        }
        public RequestBaseModel RequestBaseMdl { get; set; }
        public DemandType DemandType { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
