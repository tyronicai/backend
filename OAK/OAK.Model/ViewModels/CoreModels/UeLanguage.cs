namespace OAK.Model.Core
{
    using OAK.Model.BaseModels;
    using System.Collections.Generic;

    public class UeLanguage : LocalizationModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CultureName { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}