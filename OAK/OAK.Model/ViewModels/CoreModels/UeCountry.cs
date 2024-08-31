using OAK.Model.BaseModels;
using System.Collections.Generic;

namespace OAK.Model.ViewModels.CoreModels
{
    public class UeCountry : LocalizationModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IsoCode2 { get; set; }
        public string IsoCode3 { get; set; }
        public string CultureName { get; set; }
        public string CountryCode { get; set; }
        public int CountryCodeLength { get; set; }
        public string AreaCodes { get; set; }
        public int PhoneAreaCodeMinLength { get; set; }
        public int PhoneAreaCodeMaxLength { get; set; }
        public int PhoneSubscriberNumberLengthMin { get; set; }
        public int PhoneSubscriberNumberLengthMax { get; set; }
        public bool IsActive { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}

