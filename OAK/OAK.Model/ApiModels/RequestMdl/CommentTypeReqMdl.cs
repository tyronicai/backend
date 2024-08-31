
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.CommentModels;
using OAK.Model.RequestModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.RequestMdl
{
    public class CommentTypeReqMdl
    {
        public CommentTypeReqMdl()
        {
            LanguageIdTexts = new List<LanguageIdText>();
        }
        public RequestBaseModel RequestBaseMdl { get; set; }
        public CommentType CommentType { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
