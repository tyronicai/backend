namespace OAK.Model.Localization
{
    public class LocalizationView
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string LocalKey { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string CultureName { get; set; }
    }
}