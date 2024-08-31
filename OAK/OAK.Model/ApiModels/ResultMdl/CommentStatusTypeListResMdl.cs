using OAK.Model.ResultModels;
using OAK.Model.ViewModels.CommentModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class CommentStatusTypeListResMdl
    {
        public CommentStatusTypeListResMdl()
        {
            UeCommentStatusTypeList = new List<UeCommentStatusType>();
            ResultBaseMdl = new ResultBaseModel();
        }
        public ResultBaseModel ResultBaseMdl { get; set; }
        public List<UeCommentStatusType> UeCommentStatusTypeList { get; set; }
    }
}
