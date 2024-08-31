using OAK.Model.BaseModels;
using System.Collections.Generic;

namespace OAK.Model.BusinessModels.EstateModels
{
    /// <summary>
    /// 
    /// </summary>
    public class EstatesFlat : ModelBase
    {
        public int Id { get; set; }

        public int FlatTypeId { get; set; }
        public virtual FlatType FlatType { get; set; }
        public int EstateId { get; set; }

        public int FloorOfEstate { get; set; }
        public int NumberOfRooms { get; set; }
        public int SqMtOfFloor { get; set; }

        public int? TargetFloor { get; set; }
        public virtual Estate Estate { get; set; }
        public virtual ICollection<EstatePart> EstateParts { get; set; }
    }
}
