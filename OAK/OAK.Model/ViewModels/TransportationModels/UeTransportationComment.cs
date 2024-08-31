

namespace OAK.Model.ViewModels.TransportationModels
{
    using System;

    public class UeTransportationComment
    {
        public int Id { get; set; }

        public DateTime CommentDate { get; set; }
        public int TransportationDemandId { get; set; }
        public int CommentId { get; set; }


    }
}
