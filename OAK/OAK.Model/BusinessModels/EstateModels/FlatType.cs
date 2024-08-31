using OAK.Model.BaseModels;
using OAK.Model.Core;
using System.ComponentModel.DataAnnotations;

namespace OAK.Model.BusinessModels.EstateModels
{
    public class FlatType : LocalizationModelBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "FlatType.Name.Required")]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public int? PropertyJsonId { get; set; }

        public virtual PropertyJson PropertyJson { get; set; }
    }
}
