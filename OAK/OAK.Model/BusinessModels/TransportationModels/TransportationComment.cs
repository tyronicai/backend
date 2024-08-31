

using OAK.Model.BaseModels;

namespace OAK.Model.BusinessModels.TransportationModels
{
    using OAK.Model.BusinessModels.CommentModels;
    using System;

    public class TransportationComment : ModelBase

    {
        public int Id { get; set; }

        public DateTime CommentDate { get; set; }
        public int TransportationDemandId { get; set; }
        public int CommentId { get; set; }

        public virtual Transportation Demand { get; set; }
        public virtual Comment Comment { get; set; }

    }
}
