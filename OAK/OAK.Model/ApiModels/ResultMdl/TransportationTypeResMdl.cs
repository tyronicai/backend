
using OAK.Model.BaseModels;
using OAK.Model.ResultModels;
using OAK.Model.ViewModels.TransportationModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class TransportationTypeResMdl
    {
        public TransportationTypeResMdl()
        {
            LanguageIdTexts = new List<LanguageIdText>();
        }
        public ResultBaseModel ResultBaseMdl { get; set; }
        public UeTransportationType UeTransportationType { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
