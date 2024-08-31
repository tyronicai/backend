
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.EstateModels;
using OAK.Model.BusinessModels.TransportationModels;
using OAK.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAK.WebApi.Models.RequestMdl
{
    public class TransportationTypeAddReqMdl
    {
        public TransportationTypeAddReqMdl()
        {
            languageIdTexts = new List<LanguageIdText>();
        }
        public RequestBaseModel requestBaseMdl { get; set; }
        public TransportationType transportationType { get; set; }
        public List<LanguageIdText> languageIdTexts { get; set; }
    }
}
