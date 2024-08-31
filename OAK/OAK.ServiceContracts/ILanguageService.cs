namespace OAK.ServiceContracts
{
    using OAK.Model.Core;
    using System.Collections.Generic;
    using OAK.Data;
    using OAK.Data.Paging;

    public interface ILanguageService
    {
        IUnitOfWork UnitOfWork { get; }
        IPaginate<Language> GetAll(int index, int size);
        List<Language> GetAllLanguageList();
        List<Language> GetActiveLanguageList();
        Language GetLanguage(int id);
        Language GetLanguageByCultureName(string cultureName);
        bool Add(Language language);
        bool Update(Language language);
    }


}