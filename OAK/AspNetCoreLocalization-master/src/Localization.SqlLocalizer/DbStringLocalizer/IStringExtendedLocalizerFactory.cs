using Microsoft.Extensions.Localization;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Localization.SqlLocalizer.DbStringLocalizer
{
    public interface IStringExtendedLocalizerFactory : IStringLocalizerFactory
    {
        void ResetCache();

        void ResetCache(Type resourceSource);

        IList GetImportHistory();

        IList GetExportHistory();

        IList GetLocalizationData(string reason = "export");

        IList GetLocalizationData(DateTime from, string culture = null, string reason = "export");

        void UpdatetLocalizationData(List<LocalizationRecord> data, string information);

        void AddNewLocalizationData(List<LocalizationRecord> data, string information);

    }
}
