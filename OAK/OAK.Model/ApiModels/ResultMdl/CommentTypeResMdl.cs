
using OAK.Model.BaseModels;
using OAK.Model.ResultModels;
using OAK.Model.ViewModels.CommentModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResulteMdl
{
    public class CommentTypeResMdl
    {
        public CommentTypeResMdl()
        {
            LanguageIdTexts = new List<LanguageIdText>();
        }
        public ResultBaseModel ResultBaseMdl { get; set; }
        public UeCommentType UeCommentType { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
