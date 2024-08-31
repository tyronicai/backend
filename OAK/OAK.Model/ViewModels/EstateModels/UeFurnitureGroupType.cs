
namespace OAK.Model.ViewModels.EstateModels
{
    using OAK.Model.BaseModels;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UeFurnitureGroupType : LocalizationModelBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "FurnitureGroupType.Name.Required")]
        public string Name { get; set; }


        public int? PropertyJsonId { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }

    }
}
