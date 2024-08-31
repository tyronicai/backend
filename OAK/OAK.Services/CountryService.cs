namespace OAK.Services
{
    using Microsoft.EntityFrameworkCore;
    using OAK.Data;
    using OAK.Data.Paging;
    using OAK.Model.BaseModels;
    using OAK.Model.Core;
    using OAK.ServiceContracts;
    using System.Collections.Generic;
    using System.Linq;

    public class CountryService : ICountryService
    {
        public IUnitOfWork UnitOfWork { get; }
        public ILocalizationService LocalizationService { get; }

        public CountryService(IUnitOfWork unitOfWork, ILocalizationService localizationService)
        {
            UnitOfWork = unitOfWork;
            LocalizationService = localizationService;
        }

        #region Country

        public Country Get(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<Country>().Single(predicate: x => x.Id == id);
        }

        public IPaginate<Country> GetAll(int index, int size)
        {
            IPaginate<Country> items = UnitOfWork.GetReadOnlyRepository<Country>().GetList(index: index, size: size);
            return items;
        }

        public List<Country> GetAllCountryList()
        {
            return UnitOfWork.GetReadOnlyRepository<Country>().GetAllReadOnly();
        }

        public List<Country> GetActiveCountryList()
        {
            return UnitOfWork.GetReadOnlyRepository<Country>().GetAllReadOnly(p => p.IsActive == true);
        }


        public bool Add(Country country, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(country.LocalKey, languageIdTexts);

            UnitOfWork.GetRepository<Country>().Add(country);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;
        }

        public bool Update(Country country)
        {
            Country oldRecord = UnitOfWork.GetRepository<Country>().Single(x => x.Id == country.Id);

            LocalizationService.ControlAndAdd(country.LocalKey);


            oldRecord.Name = country.Name;
            oldRecord.IsoCode2 = country.IsoCode2;
            oldRecord.IsoCode3 = country.IsoCode3;
            oldRecord.CultureName = country.CultureName;
            oldRecord.CountryCode = country.CountryCode;
            oldRecord.CountryCodeLength = country.CountryCodeLength;
            oldRecord.AreaCodes = country.AreaCodes;
            oldRecord.PhoneAreaCodeMinLength = country.PhoneAreaCodeMinLength;
            oldRecord.PhoneAreaCodeMaxLength = country.PhoneAreaCodeMaxLength;
            oldRecord.PhoneSubscriberNumberLengthMin = country.PhoneSubscriberNumberLengthMin;
            oldRecord.PhoneSubscriberNumberLengthMax = country.PhoneSubscriberNumberLengthMax;
            oldRecord.LocalKey = country.LocalKey;
            oldRecord.IsActive = country.IsActive;

            UnitOfWork.GetRepository<Country>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public bool Delete(int countryId)
        {
            Country record = UnitOfWork.GetRepository<Country>().Single(x => x.Id == countryId);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<Country>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        #endregion Country



        #region PostCodeData
        public List<PostCodeData> GetSupportedPostCodesByCountryList(int countryId)
        {
            var countryRepo = UnitOfWork.GetRepository<Country>();
            Country country = countryRepo.Single(predicate: p => p.Id == countryId,
                include: source => source.Include(x => x.SupportedPostCodes));

            if (null == country)
                return null;
            List<PostCodeData> postCodeList = UnitOfWork.GetRepository<PostCodeData>().GetAllReadOnly(x => x.IsoCountryCode == country.IsoCode2);

            List<PostCodeData> returnedPostCodeList = new List<PostCodeData>();

            foreach (var rPostCodeData in postCodeList)
            {
                foreach (var rSupportedPostCode in country.SupportedPostCodes)
                {
                    if (rPostCodeData.PostCode.Substring(0, rSupportedPostCode.PostCode.Length) == rSupportedPostCode.PostCode)
                    {
                        returnedPostCodeList.Add(rPostCodeData);
                        break;
                    }
                }

            }

            return returnedPostCodeList;
        }

        public List<PostCodeData> GetPostCodeDataListByCountryIdAndPostCode(int countryId, string postCodeStr)
        {
            var countryRepo = UnitOfWork.GetRepository<Country>();
            Country country = countryRepo.Single(predicate: p => p.Id == countryId && p.IsActive == true);

            if (null == country)
                return null;

            List<PostCodeData> postCodeList = UnitOfWork.GetReadOnlyRepository<PostCodeData>().GetAllReadOnly(x => x.IsoCountryCode == country.IsoCode2 &&
                    x.PostCode.StartsWith(postCodeStr));

            return postCodeList;
        }

        public List<PostCodeData> GetPostCodeDataListByCountryIdAndPlaceName(int countryId, string placeNameStr)
        {
            var countryRepo = UnitOfWork.GetRepository<Country>();
            Country country = countryRepo.Single(predicate: p => p.Id == countryId && p.IsActive == true);

            if (null == country)
                return null;

            List<PostCodeData> postCodeList;

            IQueryable<PostCodeData> postCodeQueryAble = UnitOfWork.GetRepository<PostCodeData>().GetQueryable(
                predicate: x => x.IsoCountryCode == country.IsoCode2 && x.PlaceName.StartsWith(placeNameStr)).OrderBy(k => k.PlaceName);


            return postCodeQueryAble.ToList();
        }

        public List<SupportedPostCode> GetSupportedPostCodes()
        {
            var supportedPostCodeRepo = UnitOfWork.GetReadOnlyRepository<SupportedPostCode>();
            return supportedPostCodeRepo.GetAllReadOnly();
        }


        #endregion PostCodeData


    }
}
