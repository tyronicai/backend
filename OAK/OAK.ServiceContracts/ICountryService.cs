namespace OAK.ServiceContracts
{
    using OAK.Model.BaseModels;
    using OAK.Model.Core;
    using System.Collections.Generic;
    using OAK.Data;
    using OAK.Data.Paging;



    public interface ICountryService
    {
        IUnitOfWork UnitOfWork { get; }
        ILocalizationService LocalizationService { get; }

        IPaginate<Country> GetAll(int index, int size);
        List<Country> GetAllCountryList();
        List<Country> GetActiveCountryList();
        bool Add(Country country, List<LanguageIdText> languageIdTexts);
        bool Update(Country country);
        Country Get(int id);
        bool Delete(int Id);


        List<PostCodeData> GetSupportedPostCodesByCountryList(int countryId);
        List<PostCodeData> GetPostCodeDataListByCountryIdAndPostCode(int countryId, string postCodeStr);
        List<PostCodeData> GetPostCodeDataListByCountryIdAndPlaceName(int countryId, string placeNameStr);
        List<SupportedPostCode> GetSupportedPostCodes();


    }

}