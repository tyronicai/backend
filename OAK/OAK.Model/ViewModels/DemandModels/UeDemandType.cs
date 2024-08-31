namespace OAK.Model.ViewModels.DemandModels
{
    using OAK.Model.BaseModels;
    using System.Collections.Generic;
    public class UeDemandType : LocalizationModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int? PropertyJsonId { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
