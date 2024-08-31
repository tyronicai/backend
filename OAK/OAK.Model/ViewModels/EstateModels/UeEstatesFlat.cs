using OAK.Model.ViewModels.EstateModels;
using System.Collections.Generic;

namespace OAK.Model.BusinessModels.EstateModels
{
    /// <summary>
    /// 
    /// </summary>
    public class UeEstatesFlat
    {
        public int Id { get; set; }
        public int EstateId { get; set; }
        public int EstateTypeId { get; set; }
        public int FlatTypeId { get; set; }
        public int FloorOfEstate { get; set; }
        public int SqMtOfFloor { get; set; }
        public int NumberOfRooms { get; set; }
        public int? TargetFloor { get; set; }
        public List<UeEstatePart> EstateParts { get; set; }

    }
}
