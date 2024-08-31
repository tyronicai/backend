
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.EstateModels;
using OAK.Model.RequestModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.RequestMdl
{
    public class EstateTypeReqMdl
    {
        public EstateTypeReqMdl()
        {
            LanguageIdTexts = new List<LanguageIdText>();
        }
        public RequestBaseModel RequestBaseMdl { get; set; }
        public EstateType EstateType { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
