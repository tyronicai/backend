namespace OAK.Model.BusinessModels.EstateModels
{
    /// <summary>
    /// Müstakil Ev, Daire, oda, garaj, depo etcs
    /// </summary>
    public class EPartTypeFrnGrpType
    {
        public int Id { get; set; }
        public int SequenceNumber { get; set; }
        public int EstatePartTypeId { get; set; }
        public int FurnitureGroupTypeId { get; set; }
    }
}
