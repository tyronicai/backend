
namespace OAK.Model.ViewModels.EstateModels
{
    using OAK.Model.BaseModels;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UeFurnitureType : LocalizationModelBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "FurnitureType.Name.Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "FurnitureType.Volume.Required")]
        public decimal Volume { get; set; }
        public bool IsActive { get; set; }
        public int FurnitureGroupTypeId { get; set; }
        public bool Assemblable { get; set; }

        public int? PropertyJsonId { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }

    }
}
