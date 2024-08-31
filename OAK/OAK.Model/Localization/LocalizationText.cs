namespace OAK.Model.Localization
{
    using OAK.Model.Core;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class LocalizationText
    {
        [Key]
        public int Id { get; set; }
        public int LanguageId { get; set; }

        /// <summary>
        /// en-Us gibi
        /// </summary>
        [Required(ErrorMessage = "LocalizationText.CultureName.Required")]
        [MaxLength(10, ErrorMessage = "LocalizationText.CultureName.MaxLength")]
        public string CultureName { get; set; }

        [Required(ErrorMessage = "LocalizationText.Key.Required")]
        [MaxLength(100, ErrorMessage = "LocalizationText.Key.MaxLength")]
        public string LocalKey { get; set; }

        [Required(ErrorMessage = "LocalizationText.LocalizationValue.Required")]
        public string Text { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "LocalizationText.ModifiedDate.Required")]
        public DateTime ModifiedDate { get; set; }

        [Required(ErrorMessage = "LocalizationText.CreateDate.Required")]
        public DateTime CreateDate { get; set; }

        public virtual LocalizationKey LocalizationKey { get; set; }
        public virtual Language Language { get; set; }
    }
}