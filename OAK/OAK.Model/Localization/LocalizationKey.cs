namespace OAK.Model.Localization
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// TODO adını degistir.
    /// </summary>
    public class LocalizationKey
    {
        [Required(ErrorMessage = "LocalizationKey.Key.Required")]
        [MaxLength(100, ErrorMessage = "LocalizationKey.Key.MaxLength")]
        public string Key { get; set; }

        [Required(ErrorMessage = "LocalizationKey.Name.Required")]
        [MaxLength(255, ErrorMessage = "LocalizationKey.Name.MaxLength")]
        public string Name { get; set; }

        [Required(ErrorMessage = "LocalizationKey.ModifiedDate.Required")]
        public DateTime ModifiedDate { get; set; }

        [Required(ErrorMessage = "LocalizationKey.CreateDate.Required")]
        public DateTime CreateDate { get; set; }
        public virtual ICollection<LocalizationText> LocalizationTexts { get; set; }
    }
}
