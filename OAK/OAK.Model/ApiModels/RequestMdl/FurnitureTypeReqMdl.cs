
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.EstateModels;
using OAK.Model.RequestModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.RequestMdl
{
    public class FurnitureTypeReqMdl
    {
        public FurnitureTypeReqMdl()
        {
            LanguageIdTexts = new List<LanguageIdText>();
        }
        public RequestBaseModel RequestBaseMdl { get; set; }
        public FurnitureType FurnitureType { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
