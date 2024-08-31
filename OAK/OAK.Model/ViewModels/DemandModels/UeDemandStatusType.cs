using OAK.Model.BaseModels;
using OAK.Model.Core;
using System.Collections.Generic;

namespace OAK.Model.ViewModels.DemandModels
{
    public class UeDemandStatusType : LocalizationModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int? PropertyJsonId { get; set; }
        public virtual PropertyJson PropertyJson { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
