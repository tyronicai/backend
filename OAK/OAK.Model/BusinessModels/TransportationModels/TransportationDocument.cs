
using OAK.Model.BaseModels;

namespace OAK.Model.BusinessModels.TransportationModels
{
    using OAK.Model.BusinessModels.DocumentModels;

    public class TransportationDocument : ModelBase
    {
        public int Id { get; set; }
        public int TransportationId { get; set; }
        public int DocumentId { get; set; }

        public virtual Transportation Transportation { get; set; }
        public virtual Document Document { get; set; }
    }
}
