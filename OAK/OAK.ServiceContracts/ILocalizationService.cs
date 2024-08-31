using Microsoft.EntityFrameworkCore.Storage;

namespace OAK.ServiceContracts
{
    using OAK.Model.BaseModels;
    using OAK.Model.Localization;
    using System.Collections.Generic;
    using OAK.Data;
    using OAK.Data.Paging;

    public interface ILocalizationService
    {
        IUnitOfWork UnitOfWork { get; }
        LocalizationKey ControlAndAdd(string localizationKey, List<LanguageIdText> languageIdTexts = null, IDbContextTransaction trans = null);
        LanguageIdText GetIdText(string localizationKey, int languageId);
        LanguageIdText GetIdText(string localizationKey, string cultureName);
        List<LanguageIdText> GetAllIdTexts(string localizationKey);

    }

}
