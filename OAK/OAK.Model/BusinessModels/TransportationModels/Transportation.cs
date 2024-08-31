using OAK.Model.BaseModels;

namespace OAK.Model.BusinessModels.TransportationModels
{
    using OAK.Model.BusinessModels.AddressModels;
    using OAK.Model.BusinessModels.DemandModels;
    using OAK.Model.BusinessModels.EstateModels;
    using System;
    using System.Collections.Generic;
    public class Transportation : ModelBase
    {
        public int Id { get; set; }
        public int DemandId { get; set; }
        public int TransportationTypeId { get; set; }
        public int TransportationStatusTypeId { get; set; }
        public int NumberOfPeople { get; set; }

        public DateTime InitialTransportationDate { get; set; }
        public DateTime FinalTransportationDate { get; set; }
        public bool DateFlexibility { get; set; }

        public int TransportationDistanceMin { get; set; }
        public int TransportationDistanceMax { get; set; }
        public int TransportationEstimatedValue { get; set; }
        public int TransportationMaxOfferedValue { get; set; }
        public int TransportationMinOfferedValue { get; set; }
        public int TransportationAverageOfferedValue { get; set; }
        public int TransportationNumberOfOffers { get; set; }

        public decimal TransportationContractValue { get; set; }
        public decimal TransportationVAT { get; set; }
        public decimal TransportationGrossValue { get; set; }
        public decimal TransportationCommission { get; set; }

        public bool IsFixedPrice { get; set; }

        public string PropertyJsonValues { get; set; }

        public int FromEstateId { get; set; }
        public int ToEstateId { get; set; }

        public virtual Estate FromEstate { get; set; }
        public virtual Estate ToEstate { get; set; }

        public int FromAddressId { get; set; }
        public virtual GenericAddress FromAddress { get; set; }

        public int ToAddressId { get; set; }

        public int? ExtraInfoLanguageId { get; set; }
        public string ExtraInfo { get; set; }
        public virtual GenericAddress ToAddress { get; set; }

        public virtual Demand Demand { get; set; }

        public ICollection<TransportationComment> TransportationComments { get; set; }
        public ICollection<TransportationDocument> TransportationDocuments { get; set; }

        public int SortByIdAscending(int id1, int id2)
        {

            return id1 - id2;
        }

        // Default comparer for Part type.
        public int CompareTo(Transportation comparePart)
        {
            // A null value means that this object is greater.
            if (comparePart == null)
                return 1;

            else
                return this.TransportationStatusTypeId.CompareTo(comparePart.TransportationStatusTypeId);
        }

    }
}
