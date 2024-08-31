namespace OAK.Model.ViewModels.TransportationModels
{
    using OAK.Model.ViewModels.EstateModels;
    using System;
    public class UeTransportation
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

        public UeEstate FromEstate { get; set; }
        public UeEstate ToEstate { get; set; }

        public int FromAddressId { get; set; }
        public int ToAddressId { get; set; }

        public virtual UeGenericAddress FromAddress { get; set; }
        public virtual UeGenericAddress ToAddress { get; set; }

        public int? ExtraInfoLanguageId { get; set; }
        public string ExtraInfo { get; set; }
    }
}
