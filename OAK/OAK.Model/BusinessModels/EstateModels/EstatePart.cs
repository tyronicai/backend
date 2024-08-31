using OAK.Model.BaseModels;
using System.Collections.Generic;

namespace OAK.Model.BusinessModels.EstateModels
{
    public class EstatePart : ModelBase
    {
        public int Id { get; set; }
        public int EstatesFlatId { get; set; }
        public int EstatePartTypeId { get; set; }
        public string PropertyValues { get; set; }
        public int? TargetFloor { get; set; }
        public virtual EstatesFlat EstatesFlat { get; set; }
        public virtual EstatePartType EstatePartType { get; set; }
        public virtual ICollection<Furniture> Furnitures { get; set; }


    }
}
