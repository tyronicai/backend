namespace OAK.Model.ViewModels.CommentModels
{
    using System;
    public class UeComment
    {
        public int Id { get; set; }
        public int CommentTypeId { get; set; }
        public int CommentStatusTypeId { get; set; }
        public int PropertyValues { get; set; }

        public DateTime CommentDate { get; set; }
        public int? ParentCommentId { get; set; }
        public string CommentNote { get; set; }
        public int CommentLike { get; set; }
        public int CommentDislike { get; set; }
        public int CommentUsefull { get; set; }
        public int CommentScore { get; set; }

    }
}
