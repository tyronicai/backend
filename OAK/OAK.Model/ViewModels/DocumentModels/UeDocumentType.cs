using OAK.Model.BaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OAK.Model.ViewModels.DocumentModels
{
    public class UeDocumentType : LocalizationModelBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "DocumentType.Name.Required")]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int? PropertyJsonId { get; set; }

        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
