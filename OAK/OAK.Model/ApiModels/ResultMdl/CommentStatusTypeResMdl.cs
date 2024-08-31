
using OAK.Model.BaseModels;
using OAK.Model.ResultModels;
using OAK.Model.ViewModels.CommentModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class CommentStatusTypeResMdl
    {
        public CommentStatusTypeResMdl()
        {
            LanguageIdTexts = new List<LanguageIdText>();
        }
        public ResultBaseModel ResultBaseModel { get; set; }
        public UeCommentStatusType UeCommentStatusType { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
