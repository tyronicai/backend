
namespace OAK.Model.BusinessModels.EstateModels
{
    using OAK.Model.BaseModels;
    using OAK.Model.Core;
    using System.ComponentModel.DataAnnotations;

    public class FurnitureType : LocalizationModelBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "FurnitureType.Name.Required")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "FurnitureType.FurnitureCalculationTypeId.Required")]
        public int FurnitureCalculationTypeId { get; set; }
        public virtual FurnitureCalculationType FurnitureCalculationType { get; set; }

        [Required(ErrorMessage = "FurnitureType.FurnitureGroupTypeId.Required")]
        public int FurnitureGroupTypeId { get; set; }
        public virtual FurnitureGroupType FurnitureGroupType { get; set; }

        [Required(ErrorMessage = "FurnitureType.Volume.Required")]
        public decimal Volume { get; set; }
        public bool Assemblable { get; set; }
        public bool IsActive { get; set; }
        public decimal? AssembleCost { get; set; }
        public decimal? DisassembleCost { get; set; }
        public decimal? FlatRate { get; set; }



        public int? PropertyJsonId { get; set; }

        public virtual PropertyJson PropertyJson { get; set; }

    }
}
