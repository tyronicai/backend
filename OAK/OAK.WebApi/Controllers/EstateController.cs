using Microsoft.AspNetCore.Cors;
using System.Threading;

namespace OAK.WebApi.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using OAK.Model.ApiModels.RequestMdl;
    using OAK.Model.ApiModels.ResultMdl;
    using OAK.Model.BusinessModels.EstateModels;
    using OAK.Model.Localization;
    using OAK.Model.ResultModels;
    using OAK.Model.StaticModels;
    using OAK.Model.ViewModels.EstateModels;
    using OAK.ServiceContracts;
    using System.Collections.Generic;
    using System.Globalization;

    [Route("api/{culture}/Estate")]
    [ApiController]
    public class EstateController : ControllerBase
    {
        private readonly IStringLocalizer<ValuesController> _stringLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IEstateService _estateService;
        private readonly ILocalizationService _localizationService;
        //ILanguageService
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EstateController(IEstateService estateService, IStringLocalizer<ValuesController> valuesLocalizerizer, IStringLocalizer<SharedResource> sharedLocalizer,
            ILocalizationService localizationService,
            ILogger<EstateController> logger, IMapper mapper)
        {
            _estateService = estateService;
            _localizationService = localizationService;
            _stringLocalizer = valuesLocalizerizer;
            _sharedLocalizer = sharedLocalizer;
            _logger = logger;
            _mapper = mapper;
        }

        #region Estate
        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllEstates")]
        public List<Estate> GetAllEstates(int index, int size)
        {

            List<Estate> estateList = _estateService.GetAllEstates(index, size).Items as List<Estate>;

            return estateList;

        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddEstate")]
        public bool AddEstate(Estate estate)
        {

            return _estateService.AddEstate(estate);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateEstate")]
        public bool UpdateEstate(Estate estate)
        {
            bool retVal = true;
            return retVal;
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetEstate")]
        public IActionResult GetEstate([FromBody] GetEstateByIdReqMdl getEstateByIdReqMdl)
        {
            var estate = _estateService.GetEstate(getEstateByIdReqMdl.Id);
            return Ok(estate);
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteEstate")]
        public bool DeleteEstate(int id)
        {
            return _estateService.DeleteEstate(id);
        }

        [Authorize] // [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetEstateDetailByEstateId")]
        public IActionResult GetEstateDetailByEstateId([FromBody] GetEstateDetailMdl estateDetailMdl)
        {
            var estateDetails = _estateService.GetEstateDetailByEstateId(estateDetailMdl.EstateId);
            return Ok(estateDetails);
        }
        #endregion Estate

        #region EstatesFlat
        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllEstatesFlats")]
        public List<EstatesFlat> GetAllEstatesFlats(int index, int size)
        {

            List<EstatesFlat> estateList = _estateService.GetAllEstatesFlats(index, size).Items as List<EstatesFlat>;

            return estateList;

        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddEstatesFlat")]
        public bool AddEstatesFlat(EstatesFlat estate)
        {

            return _estateService.AddEstatesFlat(estate);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateEstatesFlat")]
        public bool UpdateEstatesFlat(EstatesFlat estate)
        {
            bool retVal = true;
            return retVal;
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GeEstatesFlat")]
        public EstatesFlat GetEstatesFlat(int id)
        {
            return _estateService.GetEstatesFlat(id);
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteEstatesFlat")]
        public bool DeleteEstatesFlat(int id)
        {
            return _estateService.DeleteEstatesFlat(id);
        }
        #endregion EstatesFlat

        #region EstateType
        [AllowAnonymous]
        [HttpPost("GetAllEstateTypes")]
        public EstateTypeListResMdl GetAllEstateTypes()
        {
            EstateTypeListResMdl estateTypeListResMdl = new EstateTypeListResMdl();

            List<EstateType> estateTypes = _estateService.GetAllEstateTypesList();
            UeEstateType ueEstateType;
            foreach (var estateType in estateTypes)
            {

                ueEstateType = _mapper.Map<UeEstateType>(estateType);
                ueEstateType.LanguageIdTexts = _localizationService.GetAllIdTexts(ueEstateType.LocalKey);
                estateTypeListResMdl.UeEstateTypeList.Add(ueEstateType);
            }
            return estateTypeListResMdl;
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddEstateType")]
        public bool AddEstateType(EstateTypeReqMdl estateTypeAddReqMdl)
        {
            return _estateService.AddEstateType(estateTypeAddReqMdl.EstateType, estateTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateEstateType")]
        public bool UpdateEstateType(EstateTypeReqMdl estateTypeAddReqMdl)
        {
            return _estateService.UpdateEstateType(estateTypeAddReqMdl.EstateType, estateTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetEstateType")]
        public EstateType GetEstateType(int id)
        {
            return _estateService.GetEstateType(id);

        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteEstateType")]
        public bool DeleteEstateType(int id)
        {
            return _estateService.DeleteEstateType(id);

        }

        #endregion EstateType

        #region FlatType
        [AllowAnonymous]
        [HttpPost("GetAllFlatTypes")]
        public FlatTypeListResMdl GetAllFlatTypes()
        {
            FlatTypeListResMdl flatTypeListResMdl = new FlatTypeListResMdl();

            List<FlatType> flatTypes = _estateService.GetAllFlatTypesList();
            UeFlatType ueFlatType;
            foreach (var flatType in flatTypes)
            {

                ueFlatType = _mapper.Map<UeFlatType>(flatType);
                ueFlatType.LanguageIdTexts = _localizationService.GetAllIdTexts(ueFlatType.LocalKey);
                flatTypeListResMdl.UeFlatTypeList.Add(ueFlatType);
            }
            return flatTypeListResMdl;
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddEstateType")]
        public bool AddFlatType(FlatTypeReqMdl flatTypeAddReqMdl)
        {
            return _estateService.AddFlatType(flatTypeAddReqMdl.FlatType, flatTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateFlatType")]
        public bool UpdateFlatType(FlatTypeReqMdl flatTypeAddReqMdl)
        {
            return _estateService.UpdateFlatType(flatTypeAddReqMdl.FlatType, flatTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetFlatType")]
        public FlatType GetFlatType(int id)
        {
            return _estateService.GetFlatType(id);

        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteFlatType")]
        public bool DeleteFlatType(int id)
        {
            return _estateService.DeleteFlatType(id);

        }

        #endregion FlatType

        #region EstatePart
        [AllowAnonymous]
        [HttpPost("GetAllEstateParts")]
        public List<EstatePart> GetAllEstateParts(int index, int size)
        {
            return _estateService.GetAllEstateParts(index, size).Items as List<EstatePart>;
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddEstatePart")]
        public bool AddEstatePart(EstatePart estatePart)
        {
            return _estateService.AddEstatePart(estatePart);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateEstatePart")]
        public bool UpdateEstatePart(EstatePart estatePart)
        {
            return _estateService.UpdateEstatePart(estatePart);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetEstatePart")]
        public EstatePart GetEstatePart(int id)
        {
            return _estateService.GetEstatePart(id);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteEstatePart")]
        public bool DeleteEstatePart(int id)
        {
            return _estateService.DeleteEstatePart(id);
        }

        #endregion EstatePart

        #region EstatePartType
        [AllowAnonymous]
        [HttpPost("GetAllEstatePartTypes")]
        public EstatePartTypeListResMdl GetAllEstatePartTypes()
        {
            EstatePartTypeListResMdl estatePartTypeListResMdl = new EstatePartTypeListResMdl();

            List<EstatePartType> estatePartTypes = _estateService.GetAllEstatePartTypesList();
            UeEstatePartType ueEstatePartType;
            foreach (var estatePartType in estatePartTypes)
            {

                ueEstatePartType = _mapper.Map<UeEstatePartType>(estatePartType);
                ueEstatePartType.LanguageIdTexts = _localizationService.GetAllIdTexts(ueEstatePartType.LocalKey);
                estatePartTypeListResMdl.UeEstatePartTypeList.Add(ueEstatePartType);
            }
            return estatePartTypeListResMdl;
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddEstatePartType")]
        public bool AddEstatePartType(EstatePartTypeReqMdl estatePartTypeAddReqMdl)
        {
            return _estateService.AddEstatePartType(estatePartTypeAddReqMdl.EstatePartType, estatePartTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateEstatePartType")]
        public bool UpdateEstatePartType(EstatePartTypeReqMdl estatePartTypeAddReqMdl)
        {
            return _estateService.UpdateEstatePartType(estatePartTypeAddReqMdl.EstatePartType, estatePartTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetEstatePartType")]
        public EstatePartType GetEstatePartType(int id)
        {
            return _estateService.GetEstatePartType(id);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteEstatePartType")]
        public bool DeleteEstatePartType(int id)
        {
            return _estateService.DeleteEstatePartType(id);
        }

        #endregion EstatePartType

        #region FurnitureType
        [AllowAnonymous]
        [HttpPost("GetAllFurnitureTypes")]
        public FurnitureTypeListResMdl GetAllFurnitureTypes()
        {
            FurnitureTypeListResMdl furnitureTypeListResMdl = new FurnitureTypeListResMdl();

            List<FurnitureType> FurnitureTypes = _estateService.GetAllFurnitureTypesList();
            UeFurnitureType ueFurnitureType;
            foreach (var furnitureType in FurnitureTypes)
            {

                ueFurnitureType = _mapper.Map<UeFurnitureType>(furnitureType);
                ueFurnitureType.LanguageIdTexts = _localizationService.GetAllIdTexts(ueFurnitureType.LocalKey);
                furnitureTypeListResMdl.UeFurnitureTypeList.Add(ueFurnitureType);
            }
            return furnitureTypeListResMdl;
        }
        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddFurnitureType")]
        public bool AddFurnitureType(FurnitureTypeReqMdl furnitureTypeAddReqMdl)
        {
            return _estateService.AddFurnitureType(furnitureTypeAddReqMdl.FurnitureType, furnitureTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateFurnitureType")]
        public bool UpdateFurnitureType(FurnitureTypeReqMdl furnitureTypeAddReqMdl)
        {
            return _estateService.UpdateFurnitureType(furnitureTypeAddReqMdl.FurnitureType, furnitureTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetFurnitureType")]
        FurnitureType GetFurnitureType(int id)
        {
            return _estateService.GetFurnitureType(id);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteFurnitureType")]
        bool DeleteFurnitureType(int id)
        {
            return _estateService.DeleteFurnitureType(id);
        }

        #endregion FurnitureType

        #region Furniture
        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllFurnitures")]
        public List<Furniture> GetAllFurnitures(int index, int size)
        {
            return _estateService.GetAllFurnitures(index, size).Items as List<Furniture>;
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddFurniture")]
        public bool AddFurniture(Furniture furniture)
        {
            return _estateService.AddFurniture(furniture);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateFurniture")]
        public bool UpdateFurniture(Furniture furniture)
        {
            return _estateService.UpdateFurniture(furniture);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetFurniture")]
        public Furniture GetFurniture(int id)
        {
            return _estateService.GetFurniture(id);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteFurniture")]
        public bool DeleteFurniture(int id)
        {
            return _estateService.DeleteFurniture(id);
        }

        #endregion Furniture


        #region EstateTypeEpartType
        [AllowAnonymous]
        [HttpPost("GetAllEstateTypeEPartType")]
        public EstateTypeEPartTypeResMdl GetAllEstateTypeEPartType()
        {
            EstateTypeEPartTypeResMdl estateTypeEpartTypeResMdl = new EstateTypeEPartTypeResMdl();

            estateTypeEpartTypeResMdl.EstateTypeEPartTypeList = _estateService.GetAllEstateTypeEPartType();

            return estateTypeEpartTypeResMdl;
        }
        #endregion EstateTypeEpartType


        #region EPartTypeFrnGrpType
        [AllowAnonymous]
        [HttpPost("GetAllEPartTypeFrnGrpType")]
        public EPartTypeFrnGrpTypeResMdl GetAllEPartTypeFrnGrpType()
        {
            EPartTypeFrnGrpTypeResMdl ePartTypeFrnGrpTypeResMdl = new EPartTypeFrnGrpTypeResMdl();

            ePartTypeFrnGrpTypeResMdl.EPartTypeFrnGrpTypeList = _estateService.GetAllEPartTypeFrnGrpType();

            return ePartTypeFrnGrpTypeResMdl;
        }
        #endregion EPartTypeFrnType

        #region EstateMetaData 

        [HttpPost("GetEstateMetaData")]
        [AllowAnonymous]
        public EstateMetaDataResMdl GetEstateMetaData()
        {
            EstateMetaDataResMdl estateMetaDataResMdl = new EstateMetaDataResMdl();

            // Estate Types
            List<EstateType> estateTypes = _estateService.GetAllEstateTypesList();
            UeEstateType ueEstateType;
            foreach (var rec in estateTypes)
            {

                ueEstateType = _mapper.Map<UeEstateType>(rec);
                ueEstateType.LanguageIdTexts = _localizationService.GetAllIdTexts(rec.LocalKey);
                estateMetaDataResMdl.UeEstateTypeList.Add(ueEstateType);
            }

            // Flat Types
            List<FlatType> flatTypes = _estateService.GetAllFlatTypesList();
            UeFlatType ueFlatType;
            foreach (var rec in flatTypes)
            {

                ueFlatType = _mapper.Map<UeFlatType>(rec);
                ueFlatType.LanguageIdTexts = _localizationService.GetAllIdTexts(rec.LocalKey);
                estateMetaDataResMdl.UeFlatTypeList.Add(ueFlatType);
            }

            // Furniture Group Types
            List<FurnitureGroupType> furnitureGroupTypes = _estateService.GetAllFurnitureGroupTypesList();
            UeFurnitureGroupType ueFurnitureGroupType;
            foreach (var rec in furnitureGroupTypes)
            {

                ueFurnitureGroupType = _mapper.Map<UeFurnitureGroupType>(rec);
                ueFurnitureGroupType.LanguageIdTexts = _localizationService.GetAllIdTexts(rec.LocalKey);
                estateMetaDataResMdl.UeFurnitureGroupTypeList.Add(ueFurnitureGroupType);
            }

            // Furniture Types
            List<FurnitureType> furnitureTypes = _estateService.GetAllFurnitureTypesList();
            UeFurnitureType ueFurnitureType;
            foreach (var rec in furnitureTypes)
            {

                ueFurnitureType = _mapper.Map<UeFurnitureType>(rec);
                ueFurnitureType.LanguageIdTexts = _localizationService.GetAllIdTexts(rec.LocalKey);
                estateMetaDataResMdl.UeFurnitureTypeList.Add(ueFurnitureType);
            }

            // EstatePart Types
            List<EstatePartType> estatePartTypes = _estateService.GetAllEstatePartTypesList();
            UeEstatePartType ueEstatePartType;
            foreach (var estatePartType in estatePartTypes)
            {

                ueEstatePartType = _mapper.Map<UeEstatePartType>(estatePartType);
                ueEstatePartType.LanguageIdTexts = _localizationService.GetAllIdTexts(ueEstatePartType.LocalKey);
                estateMetaDataResMdl.UeEstatePartTypeList.Add(ueEstatePartType);
            }

            //EstateType EstatePartType
            estateMetaDataResMdl.EstateTypeEPartTypeList = _estateService.GetAllEstateTypeEPartType();

            // EstatePartType FurnitureType
            estateMetaDataResMdl.EPartTypeFrnGrpTypeList = _estateService.GetAllEPartTypeFrnGrpType();

            return estateMetaDataResMdl;
        }
        #endregion EstateMetaData
    }
}
