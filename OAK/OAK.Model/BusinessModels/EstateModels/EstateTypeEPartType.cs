namespace OAK.Model.BusinessModels.EstateModels
{
    /// <summary>
    /// Müstakil Ev, Daire, oda, garaj, depo etcs
    /// </summary>
    public class EstateTypeEPartType
    {
        public int Id { get; set; }

        public int SequenceNumber { get; set; }
        public int EstateTypeId { get; set; }
        public int EstatePartTypeId { get; set; }

        public virtual EstateType EstateType { get; set; }
        public virtual EstatePartType EstatePartType { get; set; }
    }
}
