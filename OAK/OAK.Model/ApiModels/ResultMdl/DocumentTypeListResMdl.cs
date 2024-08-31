using OAK.Model.ResultModels;
using OAK.Model.ViewModels.DocumentModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class DocumentTypeListResMdl
    {
        public DocumentTypeListResMdl()
        {
            UeDocumentTypeList = new List<UeDocumentType>();
            ResultBaseMdl = new ResultBaseModel();
        }
        public ResultBaseModel ResultBaseMdl { get; set; }
        public List<UeDocumentType> UeDocumentTypeList { get; set; }
    }
}
