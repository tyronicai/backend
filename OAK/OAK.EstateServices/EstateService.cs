using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.EstateModels;
using OAK.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OAK.EstateServices
{
    using OAK.Data;
    using OAK.Data.Paging;

    public class EstateService : IEstateService
    {
        public IUnitOfWork UnitOfWork { get; }
        public ILocalizationService LocalizationService { get; }

        public EstateService(IUnitOfWork unitOfWork, ILocalizationService localizationService)
        {
            UnitOfWork = unitOfWork;
            LocalizationService = localizationService;
        }

        #region Estate

        public IPaginate<Estate> GetAllEstates(int index, int size)
        {
            IPaginate<Estate> items = UnitOfWork.GetRepository<Estate>().GetList(index: index, size: size);
            return items;
        }

        public bool AddEstate(Estate estate)
        {
            UnitOfWork.GetRepository<Estate>().Add(estate);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;
        }

        public bool UpdateEstate(Estate estate)
        {
            Estate oldRecord = UnitOfWork.GetRepository<Estate>().Single(x => x.Id == estate.Id);


            //map
            oldRecord.EstateTypeId = estate.EstateTypeId;

            UnitOfWork.GetRepository<Estate>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public Estate GetEstate(int id)
        {
            return UnitOfWork.GetRepository<Estate>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteEstate(int Id)
        {
            Estate record = UnitOfWork.GetRepository<Estate>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<Estate>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public List<EstateDetailView> GetEstateDetailByEstateId(int estateId)
        {
            List<EstateDetailView> results = null;
            try
            {
                var repo = UnitOfWork.GetReadOnlyRepository<EstateDetailView>();


                var myDemands = repo.GetAllReadOnly(x => x.Id == estateId);

                results = myDemands.ToList();
            }
            catch (Exception e)
            {
                // errorOccured = true;
            }

            return results;
        }

        #endregion Estate

        #region FlatType

        public IPaginate<FlatType> GetAllFlatTypes(int index, int size)
        {
            IPaginate<FlatType> items = UnitOfWork.GetRepository<FlatType>().GetList(index: index, size: size);
            return items;
        }

        public List<FlatType> GetAllFlatTypesList()
        {
            List<FlatType> items = UnitOfWork.GetRepository<FlatType>().GetAllReadOnly();
            return items;
        }

        public bool AddFlatType(FlatType flatType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(flatType.LocalKey, languageIdTexts);

            UnitOfWork.GetRepository<FlatType>().Add(flatType);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;
        }

        public bool UpdateFlatType(FlatType flatType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(flatType.LocalKey, languageIdTexts);

            FlatType oldRecord = UnitOfWork.GetRepository<FlatType>().Single(x => x.Id == flatType.Id);


            //map
            //oldRecord.EstateTypeId = estate.EstateTypeId;

            UnitOfWork.GetRepository<FlatType>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public FlatType GetFlatType(int id)
        {
            return UnitOfWork.GetRepository<FlatType>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteFlatType(int Id)
        {
            FlatType record = UnitOfWork.GetRepository<FlatType>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<FlatType>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        #endregion FlatType

        #region EstatesFlat

        public IPaginate<EstatesFlat> GetAllEstatesFlats(int index, int size)
        {
            IPaginate<EstatesFlat> items = UnitOfWork.GetRepository<EstatesFlat>().GetList(index: index, size: size);
            return items;
        }

        public bool AddEstatesFlat(EstatesFlat estate)
        {
            UnitOfWork.GetRepository<EstatesFlat>().Add(estate);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;
        }

        public bool UpdateEstatesFlat(EstatesFlat estate)
        {
            EstatesFlat oldRecord = UnitOfWork.GetRepository<EstatesFlat>().Single(x => x.Id == estate.Id);


            //map

            UnitOfWork.GetRepository<EstatesFlat>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public EstatesFlat GetEstatesFlat(int id)
        {
            return UnitOfWork.GetRepository<EstatesFlat>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteEstatesFlat(int Id)
        {
            EstatesFlat record = UnitOfWork.GetRepository<EstatesFlat>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<EstatesFlat>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        #endregion EstatesFlat

        #region EstateType

        public IPaginate<EstateType> GetAllEstateTypes(int index, int size)
        {
            IPaginate<EstateType> items = UnitOfWork.GetRepository<EstateType>().GetList(index: index, size: size);
            return items;
        }

        public List<EstateType> GetAllEstateTypesList()
        {
            List<EstateType> items = UnitOfWork.GetRepository<EstateType>().GetAllReadOnly();
            return items;
        }

        public bool AddEstateType(EstateType estateType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(estateType.LocalKey, languageIdTexts);

            UnitOfWork.GetRepository<EstateType>().Add(estateType);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;
        }

        public bool UpdateEstateType(EstateType estateType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(estateType.LocalKey, languageIdTexts);

            EstateType oldRecord = UnitOfWork.GetRepository<EstateType>().Single(x => x.Id == estateType.Id);


            //map
            //oldRecord.EstateTypeId = estate.EstateTypeId;

            UnitOfWork.GetRepository<EstateType>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public EstateType GetEstateType(int id)
        {
            return UnitOfWork.GetRepository<EstateType>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteEstateType(int Id)
        {
            EstateType record = UnitOfWork.GetRepository<EstateType>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<EstateType>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        #endregion EstateType

        #region EstatePart

        public IPaginate<EstatePart> GetAllEstateParts(int index, int size)
        {
            IPaginate<EstatePart> items = UnitOfWork.GetRepository<EstatePart>().GetList(index: index, size: size);
            return items;
        }


        public bool AddEstatePart(EstatePart estatePart)
        {
            UnitOfWork.GetRepository<EstatePart>().Add(estatePart);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;
        }

        public bool UpdateEstatePart(EstatePart estatePart)
        {
            EstatePart oldRecord = UnitOfWork.GetRepository<EstatePart>().Single(x => x.Id == estatePart.Id);


            //map
            //oldRecord.EstateTypeId = estate.EstateTypeId;

            UnitOfWork.GetRepository<EstatePart>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public EstatePart GetEstatePart(int id)
        {
            return UnitOfWork.GetRepository<EstatePart>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteEstatePart(int Id)
        {
            EstatePart record = UnitOfWork.GetRepository<EstatePart>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<EstatePart>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        #endregion EstatePart

        #region EstatePartType

        public IPaginate<EstatePartType> GetAllEstatePartTypes(int index, int size)
        {
            IPaginate<EstatePartType> items = UnitOfWork.GetRepository<EstatePartType>()
                .GetList(index: index, size: size);
            return items;
        }

        public List<EstatePartType> GetAllEstatePartTypesList()
        {
            List<EstatePartType> items = UnitOfWork.GetRepository<EstatePartType>().GetAllReadOnly();
            return items;
        }

        public bool AddEstatePartType(EstatePartType estatePartType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(estatePartType.LocalKey, languageIdTexts);

            UnitOfWork.GetRepository<EstatePartType>().Add(estatePartType);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;
        }

        public bool UpdateEstatePartType(EstatePartType estatePartType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(estatePartType.LocalKey, languageIdTexts);
            EstatePartType oldRecord =
                UnitOfWork.GetRepository<EstatePartType>().Single(x => x.Id == estatePartType.Id);


            //map
            //oldRecord.EstateTypeId = estate.EstateTypeId;

            UnitOfWork.GetRepository<EstatePartType>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public EstatePartType GetEstatePartType(int id)
        {
            return UnitOfWork.GetRepository<EstatePartType>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteEstatePartType(int Id)
        {
            EstatePartType record = UnitOfWork.GetRepository<EstatePartType>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<EstatePartType>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        #endregion EstatePartType

        #region FurnitureCalculationType

        public IPaginate<FurnitureCalculationType> GetAllFurnitureCalculationTypes(int index, int size)
        {
            IPaginate<FurnitureCalculationType> items = UnitOfWork.GetRepository<FurnitureCalculationType>()
                .GetList(index: index, size: size);
            return items;
        }

        public List<FurnitureCalculationType> GetAllFurnitureCalculationTypesList()
        {
            List<FurnitureCalculationType>
                items = UnitOfWork.GetRepository<FurnitureCalculationType>().GetAllReadOnly();
            return items;
        }

        public bool AddFurnitureCalculationType(FurnitureCalculationType FurnitureCalculationType,
            List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(FurnitureCalculationType.LocalKey, languageIdTexts);

            UnitOfWork.GetRepository<FurnitureCalculationType>().Add(FurnitureCalculationType);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;
        }

        public bool UpdateFurnitureCalculationType(FurnitureCalculationType FurnitureCalculationType,
            List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(FurnitureCalculationType.LocalKey, languageIdTexts);
            FurnitureCalculationType oldRecord = UnitOfWork.GetRepository<FurnitureCalculationType>()
                .Single(x => x.Id == FurnitureCalculationType.Id);


            //map
            //oldRecord.EstateTypeId = estate.EstateTypeId;

            UnitOfWork.GetRepository<FurnitureCalculationType>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public FurnitureCalculationType GetFurnitureCalculationType(int id)
        {
            return UnitOfWork.GetRepository<FurnitureCalculationType>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteFurnitureCalculationType(int Id)
        {
            FurnitureCalculationType record = UnitOfWork.GetRepository<FurnitureCalculationType>()
                .Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<FurnitureCalculationType>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        #endregion FurnitureCalculationType

        #region FurnitureGroupType

        public IPaginate<FurnitureGroupType> GetAllFurnitureGroupTypes(int index, int size)
        {
            IPaginate<FurnitureGroupType> items = UnitOfWork.GetRepository<FurnitureGroupType>()
                .GetList(index: index, size: size);
            return items;
        }

        public List<FurnitureGroupType> GetAllFurnitureGroupTypesList()
        {
            List<FurnitureGroupType> items = UnitOfWork.GetRepository<FurnitureGroupType>().GetAllReadOnly();
            return items;
        }

        public bool AddFurnitureGroupType(FurnitureGroupType furnitureGroupType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(furnitureGroupType.LocalKey, languageIdTexts);

            UnitOfWork.GetRepository<FurnitureGroupType>().Add(furnitureGroupType);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;
        }

        public bool UpdateFurnitureGroupType(FurnitureGroupType furnitureGroupType,
            List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(furnitureGroupType.LocalKey, languageIdTexts);
            FurnitureGroupType oldRecord = UnitOfWork.GetRepository<FurnitureGroupType>()
                .Single(x => x.Id == furnitureGroupType.Id);


            //map
            //oldRecord.EstateTypeId = estate.EstateTypeId;

            UnitOfWork.GetRepository<FurnitureGroupType>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public FurnitureGroupType GetFurnitureGroupType(int id)
        {
            return UnitOfWork.GetRepository<FurnitureGroupType>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteFurnitureGroupType(int Id)
        {
            FurnitureGroupType record = UnitOfWork.GetRepository<FurnitureGroupType>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<FurnitureGroupType>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        #endregion FurnitureGroupType

        #region FurnitureType

        public IPaginate<FurnitureType> GetAllFurnitureTypes(int index, int size)
        {
            IPaginate<FurnitureType>
                items = UnitOfWork.GetRepository<FurnitureType>().GetList(index: index, size: size);
            return items;
        }

        public List<FurnitureType> GetAllFurnitureTypesList()
        {
            List<FurnitureType> items = UnitOfWork.GetRepository<FurnitureType>().GetAllReadOnly();
            return items;
        }

        public bool AddFurnitureType(FurnitureType furnitureType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(furnitureType.LocalKey, languageIdTexts);

            UnitOfWork.GetRepository<FurnitureType>().Add(furnitureType);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;
        }

        public bool UpdateFurnitureType(FurnitureType furnitureType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(furnitureType.LocalKey, languageIdTexts);
            FurnitureType oldRecord = UnitOfWork.GetRepository<FurnitureType>().Single(x => x.Id == furnitureType.Id);


            //map
            //oldRecord.EstateTypeId = estate.EstateTypeId;

            UnitOfWork.GetRepository<FurnitureType>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public FurnitureType GetFurnitureType(int id)
        {
            return UnitOfWork.GetRepository<FurnitureType>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteFurnitureType(int Id)
        {
            FurnitureType record = UnitOfWork.GetRepository<FurnitureType>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<FurnitureType>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        #endregion FurnitureType

        #region Furniture

        public IPaginate<Furniture> GetAllFurnitures(int index, int size)
        {
            IPaginate<Furniture> items = UnitOfWork.GetRepository<Furniture>().GetList(index: index, size: size);
            return items;
        }

        public bool AddFurniture(Furniture furniture)
        {
            UnitOfWork.GetRepository<Furniture>().Add(furniture);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;
        }

        public bool UpdateFurniture(Furniture furniture)
        {
            Furniture oldRecord = UnitOfWork.GetRepository<Furniture>().Single(x => x.Id == furniture.Id);


            //map
            //oldRecord.furniture = furniture.EstateTypeId;

            UnitOfWork.GetRepository<Furniture>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public Furniture GetFurniture(int id)
        {
            return UnitOfWork.GetRepository<Furniture>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteFurniture(int Id)
        {
            Furniture record = UnitOfWork.GetRepository<Furniture>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<Furniture>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        #endregion Furniture

        #region EstatePartFurniture

        public IPaginate<EstatePartFurniture> GetAllEstatePartFurnitures(int index, int size)
        {
            IPaginate<EstatePartFurniture> items = UnitOfWork.GetRepository<EstatePartFurniture>()
                .GetList(index: index, size: size);
            return items;
        }

        public bool AddEstatePartFurniture(EstatePartFurniture estatePartFurniture)
        {
            UnitOfWork.GetRepository<EstatePartFurniture>().Add(estatePartFurniture);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;
        }

        //public bool UpdateEstatePartFurniture(EstatePartFurniture estatePartFurniture)
        //{
        //    EstatePartFurniture oldRecord = UnitOfWork.GetRepository<EstatePartFurniture>().Single(x => x.Id == estatePartFurniture.Id);


        //    //map
        //    //oldRecord.furniture = furniture.EstateTypeId;

        //    UnitOfWork.GetRepository<EstatePartFurniture>().Update(oldRecord);
        //    int affectedRows = UnitOfWork.SaveChanges();
        //    return affectedRows > 0;
        //}

        public EstatePartFurniture GetEstatePartFurniture(int id)
        {
            return UnitOfWork.GetRepository<EstatePartFurniture>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteEstatePartFurniture(int Id)
        {
            EstatePartFurniture record = UnitOfWork.GetRepository<EstatePartFurniture>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            UnitOfWork.GetRepository<EstatePartFurniture>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        #endregion EstatePartFurniture

        #region EstateTypeEPartType

        public IPaginate<EstateTypeEPartType> GetAllEstateTypeEPartTypeMatrices(int index, int size)
        {
            IPaginate<EstateTypeEPartType> items = UnitOfWork.GetRepository<EstateTypeEPartType>()
                .GetList(index: index, size: size);
            return items;
        }

        public List<EstateTypeEPartType> GetAllEstateTypeEPartType()
        {
            return UnitOfWork.GetRepository<EstateTypeEPartType>().GetAllReadOnly();
        }

        public bool AddEstateTypeEPartType(EstateTypeEPartType estateTypeEPartType)
        {
            UnitOfWork.GetRepository<EstateTypeEPartType>().Add(estateTypeEPartType);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;
        }

        public bool UpdateEstateTypeEPartType(EstateTypeEPartType estateTypeEPartType)
        {
            EstateTypeEPartType locEstateTypeEPartType = UnitOfWork.GetRepository<EstateTypeEPartType>().Single(
                predicate: p => p.EstatePartTypeId == estateTypeEPartType.EstatePartTypeId &&
                                p.EstateTypeId == estateTypeEPartType.EstateTypeId,
                disableTracking: false);

            if (null != locEstateTypeEPartType)
            {
                locEstateTypeEPartType.SequenceNumber = estateTypeEPartType.SequenceNumber;
                UnitOfWork.GetRepository<EstateTypeEPartType>().Update(locEstateTypeEPartType);
            }

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public EstateTypeEPartType GetEstateTypeEPartType(int id)
        {
            return UnitOfWork.GetRepository<EstateTypeEPartType>().Single(predicate: x => x.Id == id);
        }

        public EstateTypeEPartType GetEstateTypeEPartTypeByTypeId(int estateTypeId, int estatePartTypeId)
        {
            return UnitOfWork.GetRepository<EstateTypeEPartType>().Single(
                predicate: x => x.EstateTypeId == estateTypeId && x.EstatePartTypeId == estatePartTypeId);
        }

        public bool DeleteEstateTypeEPartType(int Id)
        {
            EstateTypeEPartType record = UnitOfWork.GetRepository<EstateTypeEPartType>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            UnitOfWork.GetRepository<EstateTypeEPartType>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        #endregion EstateTypeEPartType

        #region EPartTypeFrnGrpType

        public IPaginate<EPartTypeFrnGrpType> GetAllEPartTypeFrnGrpTypeMatrices(int index, int size)
        {
            IPaginate<EPartTypeFrnGrpType> items = UnitOfWork.GetRepository<EPartTypeFrnGrpType>()
                .GetList(index: index, size: size);
            return items;
        }

        public List<EPartTypeFrnGrpType> GetAllEPartTypeFrnGrpType()
        {
            return UnitOfWork.GetRepository<EPartTypeFrnGrpType>().GetAllReadOnly();
        }

        public bool AddEPartTypeFrnGrpType(EPartTypeFrnGrpType ePartTypeFrnGrpType)
        {
            UnitOfWork.GetRepository<EPartTypeFrnGrpType>().Add(ePartTypeFrnGrpType);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;
        }

        public bool UpdateEPartTypeFrnGrpType(EPartTypeFrnGrpType ePartTypeFrnGrpType)
        {
            EPartTypeFrnGrpType locEPartTypeFrnType = UnitOfWork.GetRepository<EPartTypeFrnGrpType>().Single(
                predicate: p => p.EstatePartTypeId == ePartTypeFrnGrpType.EstatePartTypeId &&
                                p.FurnitureGroupTypeId == ePartTypeFrnGrpType.FurnitureGroupTypeId,
                disableTracking: false);

            if (null != locEPartTypeFrnType)
            {
                locEPartTypeFrnType.SequenceNumber = ePartTypeFrnGrpType.SequenceNumber;
                UnitOfWork.GetRepository<EPartTypeFrnGrpType>().Update(locEPartTypeFrnType);
            }

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public EPartTypeFrnGrpType GetEPartTypeFrnGrpType(int id)
        {
            return UnitOfWork.GetRepository<EPartTypeFrnGrpType>().Single(predicate: x => x.Id == id);
        }

        public EPartTypeFrnGrpType GetEPartTypeFrnGrpTypeByTypeId(int estatePartTypeId, int furnitureGroupTypeId)
        {
            return UnitOfWork.GetRepository<EPartTypeFrnGrpType>().Single(
                predicate: x =>
                    x.EstatePartTypeId == estatePartTypeId && x.FurnitureGroupTypeId == furnitureGroupTypeId);
        }

        public bool DeleteEPartTypeFrnGrpType(int Id)
        {
            EPartTypeFrnGrpType record = UnitOfWork.GetRepository<EPartTypeFrnGrpType>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            UnitOfWork.GetRepository<EPartTypeFrnGrpType>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        #endregion EPartTypeFrnGrpType
    }
}