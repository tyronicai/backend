using OAK.Model.BaseModels;
using System.Collections.Generic;

namespace OAK.Model.BusinessModels.EstateModels
{
    /// <summary>
    /// 
    /// </summary>
    public class Estate : ModelBase
    {
        public int Id { get; set; }
        public int EstateTypeId { get; set; }
        public virtual EstateType EstateType { get; set; }

        public string PropertyValues { get; set; }
        public int NumberOfFloors { get; set; } //1: Single, 2:Dublex, 3: Triplex, 4:...

        public int NumberOfRooms { get; set; }
        public int TotalSquareMeter { get; set; }
        public int ElevatorAvailability { get; set; } // 0: No, 1: Person, 2: Freight
        public bool WaitingPermission { get; set; } //0: not wending; 1: Clent will arrange, 2: Company will arrange
        public bool FurnitureMontage { get; set; }
        public bool KitchenMontage { get; set; }
        public bool PackingService { get; set; }

        public bool HasLoft { get; set; }
        public bool HasGardenGarage { get; set; }
        public bool HasCellar { get; set; }
        public int LoftFloor { get; set; }
        public int GardenGarageFloor { get; set; }
        public int CellarFloor { get; set; }
        public int LoftSqMt { get; set; }
        public int GardenGarageSqMt { get; set; }
        public int CellarSqMt { get; set; }
        public virtual ICollection<EstatesFlat> Flats { get; set; }
    }
}
