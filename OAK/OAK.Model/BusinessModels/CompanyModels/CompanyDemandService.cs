using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.DemandModels;

namespace OAK.Model.BusinessModels.CompanyModels
{
    public class CompanyDemandService : ModelBase
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }

        public int DemandStatusTypeId { get; set; }
        public int DemandId { get; set; }
        public decimal OfferAmount { get; set; }

        public virtual Company Company { get; set; }
        public virtual Demand Demand { get; set; }

    }
}
