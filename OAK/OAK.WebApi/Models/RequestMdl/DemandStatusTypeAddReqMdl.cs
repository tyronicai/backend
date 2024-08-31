
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.DemandModels;
using OAK.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAK.WebApi.Models.RequestMdl
{
    public class DemandStatusTypeAddReqMdl
    {
        public DemandStatusTypeAddReqMdl()
        {
            languageIdTexts = new List<LanguageIdText>();
        }
        public RequestBaseModel requestBaseMdl { get; set; }
        public DemandStatusType demandStatusType { get; set; }
        public List<LanguageIdText> languageIdTexts { get; set; }
    }
}
