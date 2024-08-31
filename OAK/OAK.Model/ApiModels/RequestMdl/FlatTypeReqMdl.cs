
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.EstateModels;
using OAK.Model.RequestModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.RequestMdl
{
    public class FlatTypeReqMdl
    {
        public FlatTypeReqMdl()
        {
            LanguageIdTexts = new List<LanguageIdText>();
        }
        public RequestBaseModel RequestBaseMdl { get; set; }
        public FlatType FlatType { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
