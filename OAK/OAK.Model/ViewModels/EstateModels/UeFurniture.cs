namespace OAK.Model.ViewModels.EstateModels
{
    public class UeFurniture
    {
        public int Id { get; set; }
        public int FurnitureTypeId { get; set; }
        public int? NumberOfFurnitures { get; set; }
        public bool DoAssemble { get; set; }
        public int? TargetFloor { get; set; }
        public string PropertyValues { get; set; }
    }
}
