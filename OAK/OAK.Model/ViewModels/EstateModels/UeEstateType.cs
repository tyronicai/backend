namespace OAK.Model.ViewModels.EstateModels
{
    using OAK.Model.BaseModels;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    /// <summary>
    /// Estate Types:
    ///     - Wohnung:
    ///         - Property:
    ///             - InsideFlats: RadioButton :1. Single 2. Dublex 3. Triplex

    ///             - 
    ///             
    /// </summary>
    public class UeEstateType : LocalizationModelBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "EstateType.Name.Required")]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public int? PropertyJsonId { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
