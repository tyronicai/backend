namespace OAK.Model.BaseModels
{
    public class LanguageIdText : ModelBase
    {
        public int LanguageId { get; set; }
        public string CultureName { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
}
