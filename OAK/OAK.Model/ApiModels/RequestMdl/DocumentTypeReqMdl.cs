
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.DocumentModels;
using OAK.Model.RequestModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.RequestMdl
{
    public class DocumentTypeReqMdl
    {
        public DocumentTypeReqMdl()
        {
            LanguageIdTexts = new List<LanguageIdText>();
        }
        public RequestBaseModel RequestBaseMdl { get; set; }
        public DocumentType DocumentType { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
