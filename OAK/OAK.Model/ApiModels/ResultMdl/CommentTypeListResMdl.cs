using OAK.Model.ResultModels;
using OAK.Model.ViewModels.CommentModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class CommentTypeListResMdl
    {
        public CommentTypeListResMdl()
        {
            UeCommentTypeList = new List<UeCommentType>();
            ResultBaseMdl = new ResultBaseModel();
        }
        public ResultBaseModel ResultBaseMdl { get; set; }
        public List<UeCommentType> UeCommentTypeList { get; set; }
    }
}
