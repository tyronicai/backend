
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.CompanyModels;
using OAK.Model.BusinessModels.TransportationModels;

namespace OAK.Model.BusinessModels.DemandModels
{
    using OAK.Model.Core;
    using System.Collections.Generic;

    public class Demand : ModelBase
    {
        public int Id { get; set; }

        public int DemandTypeId { get; set; }
        public virtual DemandType DemandType { get; set; }

        public int AccountId { get; set; }

        public decimal DemandEstimatedValue { get; set; }
        public decimal DemandMaxOfferedValue { get; set; }
        public decimal DemandMinOfferedValue { get; set; }
        public decimal DemandAverageOfferedValue { get; set; }
        public int DemandNumberOfOffers { get; set; }

        public int? AcceptedOfferId { get; set; }
        // public virtual CompanyDemandService CompanyDemandService { get; set; }

        public decimal DemandContractValue { get; set; }
        public decimal DemandVAT { get; set; }
        public decimal DemandGrossValue { get; set; }
        public decimal DemandCommission { get; set; }

        public virtual Account Account { get; set; }

        public int DemandStatusTypeId { get; set; }
        public virtual DemandStatusType DemandStatusType { get; set; }

        public int? DemandOwnerId { get; set; }
        public virtual DemandOwner DemandOwner { get; set; }

        public string PropertyValues { get; set; }

        public virtual ICollection<CompanyDemandService> CompanyDemandServices { get; set; }
        public virtual DemandChat DemandChat { get; set; }

        public virtual ICollection<DemandComment> DemandComments { get; set; }
        //public virtual ICollection<DemandIssue> DemandIssues { get; set; }

        public List<Transportation> Transportations { get; set; }
    }
}
