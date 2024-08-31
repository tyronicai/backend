
using OAK.Model.BaseModels;
using OAK.Model.ResultModels;
using OAK.Model.ViewModels.EstateModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class EstatePartTypeResMdl
    {
        public EstatePartTypeResMdl()
        {
            LanguageIdTexts = new List<LanguageIdText>();
        }
        public ResultBaseModel ResultBaseMdl { get; set; }
        public UeEstatePartType UeEstatePartType { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
