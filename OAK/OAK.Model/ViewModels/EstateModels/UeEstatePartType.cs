namespace OAK.Model.ViewModels.EstateModels
{
    using OAK.Model.BaseModels;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    /// <summary>
    /// Müstakil Ev, Daire, oda, garaj, depo etcs
    /// </summary>
    public class UeEstatePartType : LocalizationModelBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "EstateType.Name.Required")]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsOuterPart { get; set; }

        public int? PropertyJsonId { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
