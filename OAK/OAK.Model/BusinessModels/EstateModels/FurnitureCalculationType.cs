
namespace OAK.Model.BusinessModels.EstateModels
{
    using OAK.Model.BaseModels;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class FurnitureCalculationType : LocalizationModelBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "FurnitureCalculationType.Name.Required")]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<FurnitureType> FurnitureTypes { get; set; }

    }
}
