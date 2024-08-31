namespace OAK.Model.ViewModels.CommentModels
{
    using OAK.Model.BaseModels;
    using System.Collections.Generic;
    public class UeCommentType : LocalizationModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? PropertyJsonId { get; set; }
        public List<LanguageIdText> LanguageIdTexts { get; set; }
    }
}
