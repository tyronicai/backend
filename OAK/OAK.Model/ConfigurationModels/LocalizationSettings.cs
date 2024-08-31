namespace OAK.Model.ConfigurationModels
{
    using System.Globalization;
    public class LocalizationSettings
    {
        public string[] SupportedCultures { get; set; }
        public string DefaultRequestCulture { get; set; }
        public string DefaultRequestUICulture { get; set; }
        public CultureInfo[] SupportedCultureList
        {
            get
            {

                CultureInfo[] cultureInfos = null;
                if (SupportedCultures == null || SupportedCultures.Length == 0)
                    return cultureInfos;

                cultureInfos = new CultureInfo[SupportedCultures.Length];

                for (int i = 0; i < SupportedCultures.Length; i++)
                    cultureInfos[i] = new CultureInfo(SupportedCultures[i]);

                return cultureInfos;
            }
        }
    }
}
