
using OAK.Model.BaseModels;
using OAK.Model.ResultModels;
using OAK.Model.ViewModels.DemandModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class DemandStatusTypeResMdl
    {
        public DemandStatusTypeResMdl()
        {
            LanguageIdTexts = new List<LanguageIdText>();
        }
        public ResultBaseModel ResultBaseMdl { get; set; }
        public UeDemandStatusType UeDemandStatusType { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
