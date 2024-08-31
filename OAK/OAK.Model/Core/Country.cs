namespace OAK.Model.Core
{
    using OAK.Model.BaseModels;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Country : LocalizationModelBase
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Country.Name.Required")]
        [MaxLength(150, ErrorMessage = "Country.Name.MaxLength")]
        public string Name { get; set; }

        /// <summary>
        /// tr turkey, de germany
        /// </summary>       
        [Required(ErrorMessage = "Country.IsoCode2.Required")]
        [MaxLength(10, ErrorMessage = "Country.IsoCode2.MaxLength")]
        public string IsoCode2 { get; set; }


        /// <summary>
        /// tur turkey, deu germany
        /// </summary>       
        [Required(ErrorMessage = "Country.IsoCode3.Required")]
        [MaxLength(10, ErrorMessage = "Country.IsoCode3.MaxLength")]
        public string IsoCode3 { get; set; }

        /// <summary>
        /// tr-TR turkey de-DE germany
        /// </summary>

        [Required(ErrorMessage = "Country.CultureName.Required")]
        [MaxLength(10, ErrorMessage = "Country.CultureName.MaxLength")]
        public string CultureName { get; set; }

        /// <summary>
        /// 90 turkey 49 germany  1 usa
        /// </summary>
        [Required(ErrorMessage = "Country.CountryCode.Required")]
        [MaxLength(10, ErrorMessage = "Country.CountryCode.MaxLength")]
        public string CountryCode { get; set; }


        /// <summary>
        /// 2 turkey, 2 germany,  1 usa
        /// </summary>
        [Required(ErrorMessage = "Country.CountryCodeLength.Required")]
        [MaxLength(10, ErrorMessage = "Country.CountryCodeLength.MaxLength")]
        public int CountryCodeLength { get; set; }

        /// <summary>
        /// bağlu ülke numaraları için, zorunlu değil
        /// </summary>
        public string AreaCodes { get; set; }

        [Required(ErrorMessage = "Country.PhoneAreaCodeMinLength.Required")]
        public int PhoneAreaCodeMinLength { get; set; }

        [Required(ErrorMessage = "Country.PhoneAreaCodeMaxLength.Required")]
        public int PhoneAreaCodeMaxLength { get; set; }


        [Required(ErrorMessage = "Country.PhoneSubscriberNumberLengthMin.Required")]
        public int PhoneSubscriberNumberLengthMin { get; set; }

        [Required(ErrorMessage = "Country.PhoneSubscriberNumberLengthMax.Required")]
        public int PhoneSubscriberNumberLengthMax { get; set; }

        [Required(ErrorMessage = "Country.IsActive.Required")]
        public bool IsActive { get; set; }

        public virtual ICollection<SupportedPostCode> SupportedPostCodes { get; set; }
        public virtual ICollection<PostCodeData> PostCodes { get; set; }

        public virtual ICollection<CompanyPostCodeData> CompanyPostCodeDatas { get; set; }

    }
}
