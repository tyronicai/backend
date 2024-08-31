using System;

namespace OAK.Model.BusinessModels.EstateModels
{
    public class EstateDetailView
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int EstateTypeId { get; set; }
        public string PropertyValues { get; set; }
        public int NumberOfFloors { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfFurnitures { get; set; }
        public int TotalSquareMeter { get; set; }
        public int ElevatorAvailability { get; set; }
        public bool WaitingPermission { get; set; }
        public bool FurnitureMontage { get; set; }
        public bool KitchenMontage { get; set; }
        public bool PackingService { get; set; }
        public bool HasLoft { get; set; }
        public bool HasGardenGarage { get; set; }
        public bool HasCellar { get; set; }
        public bool DoAssemble { get; set; }
        public int LoftFloor { get; set; }
        public int GardenGarageFloor { get; set; }
        public int CellarFloor { get; set; }
        public int LoftSqMt { get; set; }
        public int GardenGarageSqMt { get; set; }
        public int CellarSqMt { get; set; }
        public string EstateName { get; set; }
        public int EstateFlatId { get; set; }
        public string EstateFlatName { get; set; }
        public int EstatePartId { get; set; }
        public string EstatePartName { get; set; }
        public int FurnitureId { get; set; }
        public string FurnitureName { get; set; }
        public string LocalKey { get; set; }
    }
}