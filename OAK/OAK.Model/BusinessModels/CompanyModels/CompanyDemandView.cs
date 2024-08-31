using OAK.Model.BusinessModels.DemandModels;
using System;

namespace OAK.Model.BusinessModels.CompanyModels
{
    public class CompanyDemandView
    {
        public int CompanyId { get; set; }
        public int DemandId { get; set; }
        public double OfferAmount { get; set; }
        public DateTime CompanyDemandModified { get; set; }
        public int DemandTypeId { get; set; }
        public int AccountId { get; set; }
        public int TransportationEstimatedValue { get; set; }
        public int TransportationMaxOfferedValue { get; set; }
        public int TransportationMinOfferedValue { get; set; }
        public int TransportationAverageOfferedValue { get; set; }
        public int TransportationNumberOfOffers { get; set; }
        public double TransportationContractValue { get; set; }
        public double TransportationVAT { get; set; }
        public double TransportationGrossValue { get; set; }
        public double TransportationCommission { get; set; }
        public int DemandStatusTypeId { get; set; }
        public int TransportationTypeId { get; set; }
        public int NumberOfPeople { get; set; }
        public DateTime InitialTransportationDate { get; set; }
        public DateTime FinalTransportationDate { get; set; }
        public bool DateFlexibility { get; set; }
        public int FromEstateId { get; set; }
        public int FromEstateTypeId { get; set; }
        public int FromEstateNumberOfFloors { get; set; }
        public int FromEstateNumberOfRooms { get; set; }
        public int FromEstateTotalSquareMeter { get; set; }
        public int FromEstateElevatorAvailability { get; set; }
        public bool FromEstateWaitingPermission { get; set; }
        public bool FromEstateFurnitureMontage { get; set; }
        public bool FromEstateKitchenMontage { get; set; }
        public int ToEstateId { get; set; }
        public int ToEstateTypeId { get; set; }
        public int ToEstateNumberOfFloors { get; set; }
        public int ToEstateNumberOfRooms { get; set; }
        public int ToEstateTotalSquareMeter { get; set; }
        public int ToEstateElevatorAvailability { get; set; }
        public bool ToEstateWaitingPermission { get; set; }
        public bool ToEstateFurnitureMontage { get; set; }
        public bool ToEstateKitchenMontage { get; set; }
        public int FromAddressId { get; set; }
        public int FromAddressCountryId { get; set; }
        public string FromAddressTown { get; set; }
        public string FromAddressStreet { get; set; }
        public string FromAddressHouseNumber { get; set; }
        public string FromAddressPostCode { get; set; }
        public string FromAddressPlaceName { get; set; }
        public int FromAddressGenericAddressTypeId { get; set; }
        public int ToAddressId { get; set; }
        public int ToAddressCountryId { get; set; }
        public string ToAddressTown { get; set; }
        public string ToAddressStreet { get; set; }
        public string ToAddressHouseNumber { get; set; }
        public string ToAddressPostCode { get; set; }
        public string ToAddressPlaceName { get; set; }
        public int ToAddressGenericAddressTypeId { get; set; }


        public virtual Company Company { get; set; }
        public virtual Demand Demand { get; set; }

    }
}