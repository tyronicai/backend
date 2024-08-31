namespace OAK.Localizer.DbStringLocalizer
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Options;
    using OAK.Model.Core;
    using OAK.Model.Localization;
    using System;
    using System.Globalization;
    using System.Linq;

    // >dotnet ef migrations add LocalizationMigration
    public class DevelopmentSetup
    {
        private readonly LocalizationModelContext _context;
        private readonly IOptions<SqlLocalizationOptions> _options;
        private IOptions<RequestLocalizationOptions> _requestLocalizationOptions;

        public DevelopmentSetup(
           LocalizationModelContext context,
           IOptions<SqlLocalizationOptions> localizationOptions,
           IOptions<RequestLocalizationOptions> requestLocalizationOptions)
        {
            _options = localizationOptions;
            _context = context;
            _requestLocalizationOptions = requestLocalizationOptions;
        }

        public LocalizationText AddNewLocalizedItem(string key, CultureInfo culture, string resourceKey)
        {
            LocalizationText localizationText = null;
            if (_requestLocalizationOptions.Value.SupportedCultures.Contains(culture))
            {
                string computedKey = $"{key}.{culture}";

                LocalizationKey localizationKey = _context.LocalizationKeys.Where(x => x.Key == key).FirstOrDefault();
                Language language = _context.Languages.Where(x => x.CultureName == culture.Name).FirstOrDefault();

                lock (_context)
                {
                    DateTime dateTimeNow = DateTime.Now;

                    bool contextNeedToSave = false;

                    if (language == null)
                    {
                        var langLocalization = new LocalizationKey()
                        {
                            Key = culture.DisplayName,
                            Name = culture.DisplayName,
                            CreateDate = dateTimeNow,
                            ModifiedDate = dateTimeNow
                        };
                        _context.LocalizationKeys.Add(langLocalization);

                        language = new Language()
                        {
                            CultureName = culture.Name,
                            Name = culture.DisplayName,
                            LocalKey = langLocalization.Name
                        };
                        _context.Languages.Add(language);
                        contextNeedToSave = true;
                    }

                    if (localizationKey == null)
                    {
                        localizationKey = new LocalizationKey()
                        {
                            Key = key,
                            Name = key,
                            CreateDate = dateTimeNow,
                            ModifiedDate = dateTimeNow
                        };

                        _context.LocalizationKeys.Add(localizationKey);
                        contextNeedToSave = true;
                    }

                    localizationText = _context.LocalizationTexts.Where(x => x.LanguageId == language.Id && x.LocalKey == key).FirstOrDefault();

                    if (localizationText == null)
                    {
                        localizationText = new LocalizationText()
                        {
                            LanguageId = language.Id,
                            CultureName = language.CultureName,
                            LocalKey = key,
                            Text = computedKey,
                            Description = String.Empty,
                            CreateDate = dateTimeNow,
                            ModifiedDate = dateTimeNow
                        };
                        _context.LocalizationTexts.Add(localizationText);
                        contextNeedToSave = true;
                    }

                    if (contextNeedToSave)
                        _context.SaveChanges();
                }
            }
            return localizationText;
        }
    }
}