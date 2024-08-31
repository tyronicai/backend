using OAK.Model.BaseModels;

namespace OAK.Model.BusinessModels.EstateModels
{
    public class Furniture : ModelBase
    {
        public int Id { get; set; }
        public int FurnitureTypeId { get; set; }
        public int EstatePartId { get; set; }
        public string PropertyValues { get; set; }
        public int NumberOfFurnitures { get; set; }
        public bool DoAssemble { get; set; }
        public int? TargetFloor { get; set; }
        public virtual EstatePart EstatePart { get; set; }
    }
}
