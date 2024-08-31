
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.CommentModels;
using OAK.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAK.WebApi.Models.RequestMdl
{
    public class CommentTypeAddReqMdl
    {
        public CommentTypeAddReqMdl()
        {
            languageIdTexts = new List<LanguageIdText>();
        }
        public RequestBaseModel requestBaseMdl { get; set; }
        public CommentType commentType { get; set; }
        public List<LanguageIdText> languageIdTexts { get; set; }
    }
}
