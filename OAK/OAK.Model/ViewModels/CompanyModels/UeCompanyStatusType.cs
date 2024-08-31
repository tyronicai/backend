using OAK.Model.BaseModels;
using System.Collections.Generic;

namespace OAK.Model.ViewModels.CompanyModels
{
    public class UeCompanyStatusType : LocalizationModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? PropertyJsonId { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }


    }
}
