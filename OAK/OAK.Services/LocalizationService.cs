using Microsoft.EntityFrameworkCore.Storage;

namespace OAK.Services
{
    using OAK.Data;
    using OAK.Model.BaseModels;
    using OAK.Model.Core;
    using OAK.Model.Localization;
    using OAK.ServiceContracts;
    using System;
    using System.Collections.Generic;
    public class LocalizationService : ILocalizationService
    {
        public IUnitOfWork UnitOfWork { get; }
        public LocalizationService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public LocalizationKey ControlAndAdd(string localizationKey, List<LanguageIdText> languageIdTexts, IDbContextTransaction trans = null)
        {
            LocalizationText localizationText;
            bool localTrans = null == trans;
            trans = UnitOfWork.BeginTransaction(trans);
            var repo = UnitOfWork.GetRepository<LocalizationKey>();

            LocalizationKey loc = repo.Single(predicate: x => x.Key == localizationKey);
            if (loc == null)
            {
                DateTime dateTime = DateTime.Now;

                loc = new LocalizationKey()
                {
                    Key = localizationKey,
                    Name = localizationKey,
                    CreateDate = dateTime,
                    ModifiedDate = dateTime
                };

                repo.Add(loc);
                UnitOfWork.SaveChanges();
                //loc = repo.Single(predicate: x => x.Key == localizationKey);
            }

            if (languageIdTexts != null)
            {

                var repoLocText = UnitOfWork.GetRepository<LocalizationText>();
                foreach (var rLanguageIdtext in languageIdTexts)
                {
                    if (null == (localizationText = repoLocText.Single(predicate: t => t.LocalKey == loc.Key && t.LanguageId == rLanguageIdtext.LanguageId)))
                    {
                        localizationText = new LocalizationText()
                        {
                            LanguageId = rLanguageIdtext.LanguageId,
                            CultureName = rLanguageIdtext.CultureName,
                            LocalKey = loc.Key,
                            Text = rLanguageIdtext.Text,
                            Description = rLanguageIdtext.Description,
                            CreateDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        };
                        repoLocText.Add(localizationText);
                        UnitOfWork.SaveChanges();
                    }
                    else if (localizationText.Text != rLanguageIdtext.Text)
                    {
                        localizationText.Text = rLanguageIdtext.Text;
                        localizationText.Description = rLanguageIdtext.Description;
                        repoLocText.Update(localizationText);
                        UnitOfWork.SaveChanges();
                    }
                }
            }
            if (localTrans)
                UnitOfWork.CommitTransaction(trans);

            return loc;
        }

        public LanguageIdText GetIdText(string localizationKey, int languageId)
        {
            var localizationText = UnitOfWork.GetReadOnlyRepository<LocalizationText>()
                .Single(l => l.LocalKey == localizationKey && l.LanguageId == languageId);
            return new LanguageIdText()
            {
                LanguageId = localizationText.LanguageId,
                Text = localizationText.Text,
                Description = localizationText.Description
            };
        }

        public LanguageIdText GetIdText(string localizationKey, string cultureName)
        {
            var language = UnitOfWork.GetReadOnlyRepository<Language>().Single(la => la.CultureName == cultureName);
            if (null == language)
                return null;

            var localizationText = UnitOfWork.GetReadOnlyRepository<LocalizationText>()
                .Single(l => l.LocalKey == localizationKey && l.LanguageId == language.Id);
            if (null == localizationText)
                return null;

            return new LanguageIdText()
            {
                LanguageId = localizationText.LanguageId,
                Text = localizationText.Text,
                Description = localizationText.Description
            };
        }

        public List<LanguageIdText> GetAllIdTexts(string localizationKey)
        {
            var localizationTextList = UnitOfWork.GetReadOnlyRepository<LocalizationText>().GetAllReadOnly(l => l.LocalKey == localizationKey);
            var languageIdTextList = new List<LanguageIdText>();
            foreach (var localizationText in localizationTextList)
            {
                languageIdTextList.Add(new LanguageIdText()
                {
                    LanguageId = localizationText.LanguageId,
                    Text = localizationText.Text,
                    Description = localizationText.Description
                });
            }

            return languageIdTextList;
        }
    }
}
