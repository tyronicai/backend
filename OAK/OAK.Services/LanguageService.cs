namespace OAK.Services
{
    using OAK.Data;
    using OAK.Data.Paging;
    using OAK.Model.Core;
    using OAK.ServiceContracts;
    using System.Collections.Generic;
    public class LanguageService : ILanguageService
    {
        public IUnitOfWork UnitOfWork { get; }
        public ILocalizationService LocalizationService { get; }

        public LanguageService(IUnitOfWork unitOfWork, ILocalizationService localizationService)
        {
            UnitOfWork = unitOfWork;
            LocalizationService = localizationService;
        }

        public IPaginate<Language> GetAll(int index = 0, int size = 20)
        {
            IPaginate<Language> accounts = UnitOfWork.GetReadOnlyRepository<Language>().GetList(index: index, size: size);
            return accounts;
        }

        public List<Language> GetAllLanguageList()
        {
            return UnitOfWork.GetReadOnlyRepository<Language>().GetAllReadOnly();
        }

        public List<Language> GetActiveLanguageList()
        {
            return UnitOfWork.GetReadOnlyRepository<Language>().GetAllReadOnly(l => l.IsActive == true);
        }

        public Language GetLanguage(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<Language>().Single(predicate: x => x.Id == id);
        }

        public Language GetLanguageByCultureName(string cultureName)
        {
            return UnitOfWork.GetReadOnlyRepository<Language>().Single(predicate: x => x.CultureName == cultureName);
        }

        public bool Add(Language language)
        {
            LocalizationService.ControlAndAdd(language.LocalKey);

            UnitOfWork.GetRepository<Language>().Add(language);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;
        }

        public bool Update(Language language)
        {
            var repo = UnitOfWork.GetRepository<Language>();

            Language oldItem = repo.Single(predicate: x => x.Id == language.Id);

            if (oldItem == null)
                return false;

            LocalizationService.ControlAndAdd(language.LocalKey);
            oldItem.Name = language.Name;
            oldItem.LocalKey = language.LocalKey;
            oldItem.CultureName = language.CultureName;

            repo.Update(oldItem);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;
        }
    }
}