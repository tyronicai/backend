
using OAK.Model.BaseModels;

namespace OAK.Model.BusinessModels.DemandModels
{
    using OAK.Model.BusinessModels.CommentModels;
    using System;
    public class DemandComment : ModelBase
    {
        public int Id { get; set; }

        public DateTime CommentDate { get; set; }
        public int DemandId { get; set; }
        public int CommentId { get; set; }

        public virtual Demand Demand { get; set; }
        public virtual Comment Comment { get; set; }

    }
}
