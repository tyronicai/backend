
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.EstateModels;
using OAK.Model.RequestModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.RequestMdl
{
    public class EstatePartTypeReqMdl
    {
        public EstatePartTypeReqMdl()
        {
            LanguageIdTexts = new List<LanguageIdText>();
        }
        public RequestBaseModel RequestBaseMdl { get; set; }
        public EstatePartType EstatePartType { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
