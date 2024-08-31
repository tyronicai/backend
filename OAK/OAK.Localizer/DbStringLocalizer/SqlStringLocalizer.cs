namespace OAK.Localizer.DbStringLocalizer
{
    using Microsoft.Extensions.Localization;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class SqlStringLocalizer : IStringLocalizer
    {
        private readonly Dictionary<string, string> _localizations;

        private readonly DevelopmentSetup _developmentSetup;
        private readonly string _resourceKey;
        private readonly bool _returnKeyOnlyIfNotFound;
        private readonly bool _createNewRecordWhenLocalisedStringDoesNotExist;

        public SqlStringLocalizer(Dictionary<string, string> localizations, DevelopmentSetup developmentSetup, string resourceKey, bool returnKeyOnlyIfNotFound, bool createNewRecordWhenLocalisedStringDoesNotExist)
        {
            _localizations = localizations;
            _developmentSetup = developmentSetup;
            _resourceKey = resourceKey;
            _returnKeyOnlyIfNotFound = returnKeyOnlyIfNotFound;
            _createNewRecordWhenLocalisedStringDoesNotExist = createNewRecordWhenLocalisedStringDoesNotExist;
        }
        public LocalizedString this[string name]
        {
            get
            {
                bool notSucceed;
                var text = GetText(name, out notSucceed);

                return new LocalizedString(name, text, notSucceed);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                return this[name];
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();

        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string GetText(string key, out bool notSucceed)
        {

#if NET451
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
#elif NET46
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
#else
            var culture = CultureInfo.CurrentCulture;
#endif

            string computedKey = $"{key}.{culture}";

            string result;
            if (_localizations.TryGetValue(computedKey, out result))
            {
                notSucceed = false;
                return result;
            }
            else
            {
                notSucceed = true;
                if (_createNewRecordWhenLocalisedStringDoesNotExist)
                {
                    var localizationText = _developmentSetup.AddNewLocalizedItem(key, culture, _resourceKey);
                    _localizations.TryAdd(computedKey, localizationText.Text);
                    return localizationText.Text;
                }

                if (_returnKeyOnlyIfNotFound)
                    return key;

                return _resourceKey + "." + computedKey;
            }
        }
    }
}
