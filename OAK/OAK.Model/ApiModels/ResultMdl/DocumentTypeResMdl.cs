
using OAK.Model.BaseModels;
using OAK.Model.ResultModels;
using OAK.Model.ViewModels.DocumentModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class DocumentTypeResMdl
    {
        public DocumentTypeResMdl()
        {
            LanguageIdTexts = new List<LanguageIdText>();
        }
        public ResultBaseModel ResultBaseMdl { get; set; }
        public UeDocumentType UeDocumentType { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
