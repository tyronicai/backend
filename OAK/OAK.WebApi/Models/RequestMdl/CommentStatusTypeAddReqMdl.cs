
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.CommentModels;
using OAK.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAK.WebApi.Models.RequestMdl
{
    public class CommentStatusTypeAddReqMdl
    {
        public CommentStatusTypeAddReqMdl()
        {
            languageIdTexts = new List<LanguageIdText>();
        }
        public RequestBaseModel requestBaseMdl { get; set; }
        public CommentStatusType commentStatusType { get; set; }
        public List<LanguageIdText> languageIdTexts { get; set; }
    }
}
