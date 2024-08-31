
using OAK.Model.BaseModels;
using OAK.Model.ResultModels;
using OAK.Model.ViewModels.DemandModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class DemandTypeResMdl
    {
        public DemandTypeResMdl()
        {
            LanguageIdTexts = new List<LanguageIdText>();
        }
        public ResultBaseModel ResultBaseMdl { get; set; }
        public UeDemandType UeDemandType { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
