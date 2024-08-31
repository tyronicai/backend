using System.Collections.Generic;

namespace OAK.Model.ViewModels.EstateModels
{
    public class UeEstatePart
    {
        public int Id { get; set; }
        public int EstateId { get; set; }
        public int EstatePartTypeId { get; set; }
        public int? TargetFloor { get; set; }
        public string PropertyValues { get; set; }
        public List<UeFurniture> Furnitures { get; set; }
    }
}
