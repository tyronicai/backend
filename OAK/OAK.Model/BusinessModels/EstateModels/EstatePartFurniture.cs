using OAK.Model.BaseModels;
using System.Collections.Generic;

namespace OAK.Model.BusinessModels.EstateModels
{
    public class EstatePartFurniture : ModelBase
    {
        public int Id { get; set; }
        public int EstatePartId { get; set; }
        public int FurnitureId { get; set; }

        public virtual ICollection<EstatePart> EstateParts { get; set; }

    }
}
