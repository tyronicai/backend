using Microsoft.EntityFrameworkCore.Storage;
using OAK.Model.ApiModels.RequestMdl;
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.AddressModels;
using System.Collections.Generic;
using OAK.Data;
using OAK.Data.Paging;

namespace OAK.ServiceContracts
{
    public interface IGenericAddressService
    {
        IUnitOfWork UnitOfWork { get; }
        ILocalizationService LocalizationService { get; }

        #region GenericAddress

        IPaginate<GenericAddress> GetAllGenericAddresses();
        IPaginate<GenericAddress> GetAllGenericAddresses(int index, int size);
        GenericAddress AddGenericAddress(GenericAddress genericAddress, IDbContextTransaction trans = null);
        GenericAddress UpdateGenericAddress(GenericAddress genericAddress, IDbContextTransaction trans = null);
        GenericAddress GetGenericAddress(GetGenericAddressByIdReqMdl genericAddressByIdReqMdl);
        bool DeleteGenericAddress(int Id);
        #endregion GenericAddress

        #region GenericAddressType
        IPaginate<GenericAddressType> GetAllGenericAddressTypes(int index, int size);
        List<GenericAddressType> GetAllGenericAddressTypesList();
        bool AddGenericAddressType(GenericAddressType genericAddressType, List<LanguageIdText> languageIdTexts);
        bool UpdateGenericAddressType(GenericAddressType genericAddressType, List<LanguageIdText> languageIdTexts);
        GenericAddressType GetGenericAddressType(int id);
        bool DeleteGenericAddressType(int Id);
        #endregion GenericAddressType

    }


}
