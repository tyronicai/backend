using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using OAK.Model.ApiModels.RequestMdl;
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.AddressModels;
using OAK.ServiceContracts;
using System;
using System.Collections.Generic;

namespace OAK.Services
{
    using OAK.Data;
    using OAK.Data.Paging;

    public class GenericAddressService : IGenericAddressService
    {
        public IUnitOfWork UnitOfWork { get; }
        public ILocalizationService LocalizationService { get; }
        private readonly IMapper _mapper;

        public GenericAddressService(IUnitOfWork unitOfWork, ILocalizationService localizationService, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            LocalizationService = localizationService;
            _mapper = mapper;
        }

        #region GenericAddress

        public IPaginate<GenericAddress> GetAllGenericAddresses()
        {
            IPaginate<GenericAddress> items = UnitOfWork.GetReadOnlyRepository<GenericAddress>().GetList();
            return items;
        }

        public IPaginate<GenericAddress> GetAllGenericAddresses(int index, int size)
        {
            IPaginate<GenericAddress> items = UnitOfWork.GetReadOnlyRepository<GenericAddress>().GetList(index: index, size: size);
            return items;
        }

        public GenericAddress AddGenericAddress(GenericAddress genericAddress, IDbContextTransaction trans = null)
        {

            bool localTrans = null == trans;
            bool errorOccurred = false;
            trans = UnitOfWork.BeginTransaction(trans);
            var repo = UnitOfWork.GetRepository<GenericAddress>();

            try
            {
                repo.Add(genericAddress);
                UnitOfWork.SaveChanges();

            }
            catch (Exception ex)
            {
                errorOccurred = true;
            }

            if (localTrans)
            {
                if (!errorOccurred)
                {
                    UnitOfWork.CommitTransaction(trans);
                }
                else
                {
                    UnitOfWork.RollbackTransaction(trans);
                }
            }

            return genericAddress;
        }

        public GenericAddress UpdateGenericAddress(GenericAddress genericAddress, IDbContextTransaction trans = null)
        {
            bool localTrans = null == trans;
            bool errorOccurred = false;
            var repo = UnitOfWork.GetRepository<GenericAddress>();
            GenericAddress newAddress;

            if (genericAddress != null)
            {
                localTrans = null == trans;
                trans = UnitOfWork.BeginTransaction(trans);
                try
                {
                    newAddress = repo.Single(predicate: x => x.Id == genericAddress.Id);
                    newAddress = _mapper.Map<GenericAddress>(genericAddress);
                    newAddress.Modified = DateTime.Now;
                    genericAddress = newAddress;
                    repo.Update(newAddress);
                    UnitOfWork.SaveChanges();
                }
                catch (Exception ex)
                {
                    errorOccurred = true;
                }
                if (localTrans)
                {
                    if (!errorOccurred)
                    {
                        UnitOfWork.CommitTransaction(trans);
                    }
                    else
                    {
                        UnitOfWork.RollbackTransaction(trans);
                    }
                }

            }
            return genericAddress;
        }

        public GenericAddress GetGenericAddress(GetGenericAddressByIdReqMdl genericAddressByIdReqMdl)
        {
            return UnitOfWork.GetReadOnlyRepository<GenericAddress>().Single(predicate: x => x.Id == genericAddressByIdReqMdl.Id);
        }

        public bool DeleteGenericAddress(int Id)
        {
            GenericAddress record = UnitOfWork.GetRepository<GenericAddress>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<GenericAddress>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }
        #endregion GenericAddress

        #region GenericAddressType
        public IPaginate<GenericAddressType> GetAllGenericAddressTypes(int index, int size)
        {
            IPaginate<GenericAddressType> items = UnitOfWork.GetReadOnlyRepository<GenericAddressType>().GetList(index: index, size: size);
            return items;
        }

        public List<GenericAddressType> GetAllGenericAddressTypesList()
        {
            List<GenericAddressType> items = UnitOfWork.GetReadOnlyRepository<GenericAddressType>().GetAllReadOnly();
            return items;
        }

        public bool AddGenericAddressType(GenericAddressType genericAddressType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(genericAddressType.LocalKey, languageIdTexts);

            UnitOfWork.GetRepository<GenericAddressType>().Add(genericAddressType);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;

        }

        public bool UpdateGenericAddressType(GenericAddressType genericAddressType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(genericAddressType.LocalKey, languageIdTexts);

            GenericAddressType oldRecord = UnitOfWork.GetRepository<GenericAddressType>().Single(x => x.Id == genericAddressType.Id);


            //map
            //oldRecord.GenericAddressTypeId = genericAddress.GenericAddressTypeId;

            UnitOfWork.GetRepository<GenericAddressType>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;

        }
        public GenericAddressType GetGenericAddressType(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<GenericAddressType>().Single(predicate: x => x.Id == id);

        }
        public bool DeleteGenericAddressType(int Id)
        {
            GenericAddressType record = UnitOfWork.GetRepository<GenericAddressType>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<GenericAddressType>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }
        #endregion GenericAddressType


    }
}
