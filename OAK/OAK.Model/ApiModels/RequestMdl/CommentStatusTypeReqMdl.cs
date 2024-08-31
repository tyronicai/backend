
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.CommentModels;
using OAK.Model.RequestModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.RequestMdl
{
    public class CommentStatusTypeReqMdl
    {
        public CommentStatusTypeReqMdl()
        {
            LanguageIdTexts = new List<LanguageIdText>();
        }
        public RequestBaseModel RequestBaseMdl { get; set; }
        public CommentStatusType CommentStatusType { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
