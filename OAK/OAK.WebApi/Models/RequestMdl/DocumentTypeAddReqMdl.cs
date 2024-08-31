
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.DocumentModels;
using OAK.Model.BusinessModels.EstateModels;
using OAK.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAK.WebApi.Models.RequestMdl
{
    public class DocumentTypeAddReqMdl
    {
        public DocumentTypeAddReqMdl()
        {
            languageIdTexts = new List<LanguageIdText>();
        }
        public RequestBaseModel requestBaseMdl { get; set; }
        public DocumentType documentType { get; set; }
        public List<LanguageIdText> languageIdTexts { get; set; }
    }
}
