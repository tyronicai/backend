using OAK.Model.Core;
using OAK.Model.ResultModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class LanguageListResMdl
    {
        public LanguageListResMdl()
        {
            UeLanguageList = new List<UeLanguage>();
            ResultBaseMdl = new ResultBaseModel();
        }
        public ResultBaseModel ResultBaseMdl { get; set; }
        public List<UeLanguage> UeLanguageList { get; set; }
    }
}
