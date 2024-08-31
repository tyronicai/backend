namespace OAK.Localizer.DbStringLocalizer
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Options;
    using OAK.Model.Localization;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public class SqlStringLocalizerFactory : IStringLocalizerFactory, IStringExtendedLocalizerFactory
    {
        private readonly DevelopmentSetup _developmentSetup;
        private readonly LocalizationModelContext _context;
        private static readonly ConcurrentDictionary<string, IStringLocalizer> _resourceLocalizations = new ConcurrentDictionary<string, IStringLocalizer>();
        private readonly IOptions<SqlLocalizationOptions> _options;
        private const string Global = "global";

        public SqlStringLocalizerFactory(
           LocalizationModelContext context,
           DevelopmentSetup developmentSetup,
           IOptions<SqlLocalizationOptions> localizationOptions)
        {
            _options = localizationOptions ?? throw new ArgumentNullException(nameof(localizationOptions));
            _context = context ?? throw new ArgumentNullException(nameof(LocalizationModelContext));
            _developmentSetup = developmentSetup;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            var returnOnlyKeyIfNotFound = _options.Value.ReturnOnlyKeyIfNotFound;
            var createNewRecordWhenLocalisedStringDoesNotExist = _options.Value.CreateNewRecordWhenLocalisedStringDoesNotExist;
            SqlStringLocalizer sqlStringLocalizer;

            if (_resourceLocalizations.Keys.Contains(Global))
                return _resourceLocalizations[Global];

            List<LocalizationText> locals = _context.LocalizationTexts.Include(i => i.Language).ToList();
            var localizations = locals.ToDictionary(kvp => kvp.LocalKey + "." + kvp.Language.CultureName, kv => kv.Text);

            sqlStringLocalizer = new SqlStringLocalizer(localizations, _developmentSetup, Global, returnOnlyKeyIfNotFound, createNewRecordWhenLocalisedStringDoesNotExist);
            return _resourceLocalizations.GetOrAdd(Global, sqlStringLocalizer);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            var returnOnlyKeyIfNotFound = _options.Value.ReturnOnlyKeyIfNotFound;
            var createNewRecordWhenLocalisedStringDoesNotExist = _options.Value.CreateNewRecordWhenLocalisedStringDoesNotExist;
            if (_resourceLocalizations.Keys.Contains(baseName + location))
            {
                return _resourceLocalizations[baseName + location];
            }

            var sqlStringLocalizer = new SqlStringLocalizer(GetAllFromDatabaseForResource(baseName + location), _developmentSetup, baseName + location, false, false);
            return _resourceLocalizations.GetOrAdd(baseName + location, sqlStringLocalizer);
        }

        private Dictionary<string, string> GetAllFromDatabaseForResource(string resourceKey)
        {
            lock (_context)
            {
                List<LocalizationText> locals = _context.LocalizationTexts.Include(i => i.Language).ToList();
                var localizations = locals.ToDictionary(kvp => kvp.LocalKey + "." + kvp.Language.CultureName, kv => kv.Text);
                return localizations;
            }
        }
    }
}
