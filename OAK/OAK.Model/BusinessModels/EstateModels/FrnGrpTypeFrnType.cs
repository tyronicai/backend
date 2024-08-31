namespace OAK.Model.BusinessModels.EstateModels
{
    /// <summary>
    /// Müstakil Ev, Daire, oda, garaj, depo etcs
    /// </summary>
    public class FrnTypeFrnGrpType
    {
        public int Id { get; set; }

        public int SequenceNumber { get; set; }
        public int FurnitureGroupTypeId { get; set; }
        public int FurnitureTypeId { get; set; }

        public virtual FurnitureGroupType FurnitureGroupType { get; set; }
        public virtual FurnitureType FurnitureType { get; set; }
    }
}
