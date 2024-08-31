namespace OAK.Model.Core
{
    using OAK.Model.BaseModels;
    using OAK.Model.Localization;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Language : LocalizationModelBase
    {
        public int Id { get; set; }

        /// <summary>
        /// English, german vs.
        /// </summary>
        [Required(ErrorMessage = "Language.Name.Required")]
        [MaxLength(100, ErrorMessage = "Language.Name.MaxLength")]
        public string Name { get; set; }

        /// <summary>
        /// en-Us gibi
        /// </summary>
        [Required(ErrorMessage = "Language.CultureName.Required")]
        [MaxLength(10, ErrorMessage = "Language.CultureName.MaxLength")]
        public string CultureName { get; set; }

        public bool IsActive { get; set; }


        public virtual ICollection<LocalizationText> LocalizationTexts { get; set; }
    }
}