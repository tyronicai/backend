using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.EstateModels;
using System.Collections.Generic;
using OAK.Data;
using OAK.Data.Paging;

namespace OAK.ServiceContracts
{
    public interface IEstateService
    {
        IUnitOfWork UnitOfWork { get; }
        ILocalizationService LocalizationService { get; }

        #region Estate
        IPaginate<Estate> GetAllEstates(int index, int size);
        bool AddEstate(Estate estate);
        bool UpdateEstate(Estate estate);
        Estate GetEstate(int id);
        bool DeleteEstate(int Id);
        List<EstateDetailView> GetEstateDetailByEstateId(int estateId);
        #endregion Estate

        #region FlatType
        IPaginate<FlatType> GetAllFlatTypes(int index, int size);
        List<FlatType> GetAllFlatTypesList();
        bool AddFlatType(FlatType flatType, List<LanguageIdText> languageIdTexts = null);
        bool UpdateFlatType(FlatType flatType, List<LanguageIdText> languageIdTexts = null);
        FlatType GetFlatType(int id);
        bool DeleteFlatType(int Id);
        #endregion FlatType

        #region EstatesFlat
        IPaginate<EstatesFlat> GetAllEstatesFlats(int index, int size);

        bool AddEstatesFlat(EstatesFlat estate);

        bool UpdateEstatesFlat(EstatesFlat estate);

        EstatesFlat GetEstatesFlat(int id);

        bool DeleteEstatesFlat(int Id);
        #endregion EstatesFlat

        #region EstateType
        IPaginate<EstateType> GetAllEstateTypes(int index, int size);
        List<EstateType> GetAllEstateTypesList();
        bool AddEstateType(EstateType estateType, List<LanguageIdText> languageIdTexts = null);
        bool UpdateEstateType(EstateType estateType, List<LanguageIdText> languageIdTexts = null);
        EstateType GetEstateType(int id);
        bool DeleteEstateType(int Id);
        #endregion EstateType

        #region EstatePart
        IPaginate<EstatePart> GetAllEstateParts(int index, int size);
        bool AddEstatePart(EstatePart estatePart);
        bool UpdateEstatePart(EstatePart estatePart);
        EstatePart GetEstatePart(int id);
        bool DeleteEstatePart(int Id);
        #endregion EstatePart

        #region EstatePartType
        IPaginate<EstatePartType> GetAllEstatePartTypes(int index, int size);
        List<EstatePartType> GetAllEstatePartTypesList();
        bool AddEstatePartType(EstatePartType estatePartType, List<LanguageIdText> languageIdTexts = null);
        bool UpdateEstatePartType(EstatePartType estatePartType, List<LanguageIdText> languageIdTexts = null);
        EstatePartType GetEstatePartType(int id);
        bool DeleteEstatePartType(int Id);
        #endregion EstatePartType

        #region FurnitureCalculationType
        IPaginate<FurnitureCalculationType> GetAllFurnitureCalculationTypes(int index, int size);
        List<FurnitureCalculationType> GetAllFurnitureCalculationTypesList();
        bool AddFurnitureCalculationType(FurnitureCalculationType FurnitureCalculationType, List<LanguageIdText> languageIdTexts = null);
        bool UpdateFurnitureCalculationType(FurnitureCalculationType FurnitureCalculationType, List<LanguageIdText> languageIdTexts = null);
        FurnitureCalculationType GetFurnitureCalculationType(int id);
        bool DeleteFurnitureCalculationType(int Id);
        #endregion FurnitureCalculationType

        #region FurnitureType
        IPaginate<FurnitureType> GetAllFurnitureTypes(int index, int size);
        List<FurnitureType> GetAllFurnitureTypesList();
        bool AddFurnitureType(FurnitureType furnitureType, List<LanguageIdText> languageIdTexts = null);
        bool UpdateFurnitureType(FurnitureType furnitureType, List<LanguageIdText> languageIdTexts = null);
        FurnitureType GetFurnitureType(int id);
        bool DeleteFurnitureType(int Id);
        #endregion FurnitureType

        #region FurnitureGroupType
        IPaginate<FurnitureGroupType> GetAllFurnitureGroupTypes(int index, int size);
        List<FurnitureGroupType> GetAllFurnitureGroupTypesList();
        bool AddFurnitureGroupType(FurnitureGroupType furnitureGroupType, List<LanguageIdText> languageIdTexts = null);
        bool UpdateFurnitureGroupType(FurnitureGroupType furnitureGroupType, List<LanguageIdText> languageIdTexts = null);
        FurnitureGroupType GetFurnitureGroupType(int id);
        bool DeleteFurnitureGroupType(int Id);
        #endregion FurnitureGroupType

        #region Furniture
        IPaginate<Furniture> GetAllFurnitures(int index, int size);
        bool AddFurniture(Furniture furniture);
        bool UpdateFurniture(Furniture furniture);
        Furniture GetFurniture(int id);
        bool DeleteFurniture(int Id);
        #endregion Furniture

        #region EstatePartFurniture
        IPaginate<EstatePartFurniture> GetAllEstatePartFurnitures(int index, int size);
        bool AddEstatePartFurniture(EstatePartFurniture estatePartFurnitures);
        //bool UpdateEstatePartFurniture(EstatePartFurniture furniture);
        EstatePartFurniture GetEstatePartFurniture(int id);
        bool DeleteEstatePartFurniture(int Id);
        #endregion EstatePartFurniture

        #region EstateTypeEPartType
        IPaginate<EstateTypeEPartType> GetAllEstateTypeEPartTypeMatrices(int index, int size);
        List<EstateTypeEPartType> GetAllEstateTypeEPartType();
        bool AddEstateTypeEPartType(EstateTypeEPartType estateTypeEPartType);
        bool UpdateEstateTypeEPartType(EstateTypeEPartType estateTypeEPartType);
        EstateTypeEPartType GetEstateTypeEPartType(int id);
        EstateTypeEPartType GetEstateTypeEPartTypeByTypeId(int estateTypeId, int estatePartTypeId);
        bool DeleteEstateTypeEPartType(int Id);
        #endregion EstateTypeEPartType

        #region EPartTypeFrnGrpType
        IPaginate<EPartTypeFrnGrpType> GetAllEPartTypeFrnGrpTypeMatrices(int index, int size);
        List<EPartTypeFrnGrpType> GetAllEPartTypeFrnGrpType();
        bool AddEPartTypeFrnGrpType(EPartTypeFrnGrpType ePartTypeFrnGrpType);
        bool UpdateEPartTypeFrnGrpType(EPartTypeFrnGrpType ePartTypeFrnGrpType);
        EPartTypeFrnGrpType GetEPartTypeFrnGrpType(int id);
        EPartTypeFrnGrpType GetEPartTypeFrnGrpTypeByTypeId(int estatePartTypeId, int furnitureGrpTypeId);
        bool DeleteEPartTypeFrnGrpType(int Id);
        #endregion EPartTypeFrnGrpType
    }

}
