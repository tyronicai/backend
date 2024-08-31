namespace OAK.Model.BaseModels
{
    using OAK.Model.Localization;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class LocalizationExModelBase
    {
        [Required(ErrorMessage = "LocalizationKey.Key.Required")]
        [MaxLength(100, ErrorMessage = "LocalizationKey.Key.MaxLength")]
        public string LocalKey { get; set; }
        public virtual LocalizationKey LocalizationKey { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
