using System;
using System.Collections.Generic;
using System.Text;

namespace OAK.Model.ApiModels.TransportationModels
{
    public class TransportationReq
    {
        public int EstateTypeId { get; set; }
        public string PropertyValues { get; set; }
        public int FloorOfEstate { get; set; }
        public int NumberOfFloors { get; set; } //1: Single, 2:Dublex, 3: Triplex, 4:...

        public int NumberOfRooms { get; set; }
        public int TotalSquareMeter { get; set; }
        public int ElevatorAvailability { get; set; } // 0: No, 1: Person, 2: Freight
        public int WaitingPermission { get; set; } //0: not wending; 1: Clent will arrange, 2: Company will arrange
        public bool FurnitureMontage { get; set; }
        public bool KitchenMontage { get; set; }
    }
}
