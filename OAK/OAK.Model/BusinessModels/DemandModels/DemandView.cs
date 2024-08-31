using System;

namespace OAK.Model.BusinessModels.DemandModels
{
    #nullable enable
    public class DemandView
    {
        public int? id { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public int? DemandTypeId { get; set; }
        public int AccountId { get; set; }
        public int? DemandEstimatedValue { get; set; }
        public int? DemandMaxOfferedValue { get; set; }
        public int? DemandMinOfferedValue { get; set; }
        public int? DemandAverageOfferedValue { get; set; }
        public int? DemandNumberOfOffers { get; set; }
        public int? AcceptedOfferId { get; set; }
        public int? DemandContractValue { get; set; }
        public int? DemandVAT { get; set; }
        public int? DemandGrossValue { get; set; }
        public int? DemandCommission { get; set; }
        public int? DemandStatusTypeId { get; set; }
        public int? DemandOwnerId { get; set; }
        public string? PropertyValues { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public string? DemandTypeName { get; set; }
        public bool? IsActive { get; set; }
        public int? PropertyJsonId { get; set; }
        public int? Id { get; set; }
        public int? LanguageId { get; set; }
        public string? LocalKey { get; set; }
        public string? Text { get; set; }
        public string? Description { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? TransportationId { get; set; }
        public DateTime? TransportationCreated { get; set; }
        public DateTime? TransportationModified { get; set; }
        public int? DemandId { get; set; }
        public int? TransportationTypeId { get; set; }
        public int? TransportationStatusTypeId { get; set; }
        public int? NumberOfPeople { get; set; }
        public DateTime? InitialTransportationDate { get; set; }
        public DateTime? FinalTransportationDate { get; set; }
        public bool? DateFlexibility { get; set; }
        public int? TransportationDistanceMin { get; set; }
        public int? TransportationDistanceMax { get; set; }
        public int? TransportationEstimatedValue { get; set; }
        public int? TransportationMaxOfferedValue { get; set; }
        public int? TransportationMinOfferedValue { get; set; }
        public int? TransportationAverageOfferedValue { get; set; }
        public int? TransportationNumberOfOffers { get; set; }
        public int? TransportationContractValue { get; set; }
        public int? TransportationVAT { get; set; }
        public int? TransportationGrossValue { get; set; }
        public int? TransportationCommission { get; set; }
        public bool? IsFixedPrice { get; set; }
        public string? PropertyJsonValues { get; set; }
        public int FromEstateId { get; set; }
        public int? ToEstateId { get; set; }
        public int FromAddressId { get; set; }
        public int ToAddressId { get; set; }
        public int? ExtraInfoLanguageId { get; set; }
        public string? ExtraInfo { get; set; }
    }
}