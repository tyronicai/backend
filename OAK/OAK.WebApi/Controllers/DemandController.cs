using Microsoft.AspNetCore.Cors;
using System;

namespace OAK.WebApi.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using OAK.Model.ApiModels.RequestMdl;
    using OAK.Model.ApiModels.ResultMdl;
    using OAK.Model.BusinessModels.DemandModels;
    using OAK.Model.BusinessModels.TransportationModels;
    using OAK.Model.Localization;
    using OAK.Model.StaticModels;
    using OAK.Model.ViewModels.DemandModels;
    using OAK.Model.ViewModels.TransportationModels;
    using OAK.ServiceContracts;
    using System.Collections.Generic;
    using System.Globalization;

    [Route("api/{culture}/Demand")]
    [ApiController]
    public class DemandController : ControllerBase
    {
        private readonly IStringLocalizer<ValuesController> _stringLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IDemandService _demandService;
        private readonly ITransportationService _transportationService;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        private readonly ILogger Logger;
        //ILanguageService

        public DemandController(IDemandService demandService,
            ITransportationService transportationService,
            ILocalizationService localizationService,
            IStringLocalizer<ValuesController> valuesLocalizerizer, IStringLocalizer<SharedResource> sharedLocalizer,
            ILogger<DemandController> logger, IMapper mapper)
        {
            _demandService = demandService;
            _stringLocalizer = valuesLocalizerizer;
            _sharedLocalizer = sharedLocalizer;
            _localizationService = localizationService;
            _transportationService = transportationService;
            Logger = logger;
            _mapper = mapper;
        }

        #region Demand
        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllDemands")]
        public List<Demand> GetAllDemands(int index, int size)
        {

            List<Demand> demandList = _demandService.GetAllDemands(index, size).Items as List<Demand>;
            return demandList;

        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpGet("GetAllDemands")]
        public IActionResult GetAllDemands()
        {
            var demands = _demandService.GetAllDemands();
            return Ok(demands);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetDemandsById")]
        public IActionResult GetDemandsById([FromBody] Demand demand)
        {
            IList<Demand> demands = null;
            try
            {
                demands = _demandService.GetDemandsByAccountId(demand.AccountId, demand.DemandStatusTypeId);
            }
            catch (Exception ex)
            {
                var str = ex.Message;
            }

            return Ok(demands);
        }

        [Authorize]
        [HttpPost("GetTransportationsOfDemandsByAccountId")]
        public IActionResult GetTransportationsOfDemandsByAccountId([FromBody] Demand demand)
        {
            IList<Transportation> transportations = null;
            try
            {
                transportations = _demandService.GetTransportationsOfDemandsByAccountId(demand.AccountId, demand.DemandStatusTypeId <= 3);
            }
            catch (Exception ex)
            {
                var str = ex.Message;
            }

            return Ok(transportations);
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddDemand")]
        public bool AddDemand(Demand demand)
        {

            return _demandService.AddDemand(demand);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateDemand")]
        public bool UpdateDemand(Demand demand)
        {
            bool retVal = true;
            return retVal;
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetDemand")]
        public Demand GetDemand(int id)
        {
            return _demandService.GetDemand(id);
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteDemand")]
        public bool DeleteDemand(int id)
        {
            return _demandService.DeleteDemand(id);
        }
        #endregion Demand

        #region DemandType
        [AllowAnonymous]
        [Route("GetAllDemandTypes")]
        public DemandTypeListResMdl GetAllDemandTypes()
        {
            DemandTypeListResMdl resultMdl = new DemandTypeListResMdl();

            List<DemandType> DemandTypes = _demandService.GetAllDemandTypesList();
            UeDemandType ueDemandType;
            foreach (var furnitureType in DemandTypes)
            {

                ueDemandType = _mapper.Map<UeDemandType>(furnitureType);
                ueDemandType.LanguageIdTexts = _localizationService.GetAllIdTexts(ueDemandType.LocalKey);
                resultMdl.UeDemandTypeList.Add(ueDemandType);
            }
            return resultMdl;
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddDemandType")]
        public bool AddDemandType(DemandTypeReqMdl demandTypeAddReqMdl)
        {
            return _demandService.AddDemandType(demandTypeAddReqMdl.DemandType, demandTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateDemandType")]
        public bool UpdateDemandType(DemandTypeReqMdl demandTypeAddReqMdl)
        {
            return _demandService.UpdateDemandType(demandTypeAddReqMdl.DemandType, demandTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetDemandType")]
        public DemandType GetDemandType(int id)
        {
            return _demandService.GetDemandType(id);

        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteDemandType")]
        public bool DeleteDemandType(int id)
        {
            return _demandService.DeleteDemandType(id);

        }
        #endregion DemandType

        #region DemandStatusType
        [AllowAnonymous]
        [Route("GetAllDemandStatusTypes")]
        public DemandStatusTypeListResMdl GetAllDemandStatusTypes()
        {
            DemandStatusTypeListResMdl resultMdl = new DemandStatusTypeListResMdl();

            List<DemandStatusType> DemandStatusTypes = _demandService.GetAllDemandStatusTypesList();
            UeDemandStatusType ueDemandStatusType;
            foreach (var demandStatusType in DemandStatusTypes)
            {

                ueDemandStatusType = _mapper.Map<UeDemandStatusType>(demandStatusType);
                ueDemandStatusType.LanguageIdTexts = _localizationService.GetAllIdTexts(ueDemandStatusType.LocalKey);
                resultMdl.UeDemandStatusTypeList.Add(ueDemandStatusType);
            }
            return resultMdl;
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddDemandStatusType")]
        public bool AddDemandStatusType(DemandStatusTypeReqMdl demandStatusTypeAddReqMdl)
        {
            return _demandService.AddDemandStatusType(demandStatusTypeAddReqMdl.DemandStatusType, demandStatusTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateDemandStatusType")]
        public bool UpdateDemandStatusType(DemandStatusTypeReqMdl demandStatusTypeAddReqMdl)
        {
            return _demandService.UpdateDemandStatusType(demandStatusTypeAddReqMdl.DemandStatusType, demandStatusTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetDemandStatusType")]
        public DemandStatusType GetDemandStatusType(int id)
        {
            return _demandService.GetDemandStatusType(id);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteDemandStatusType")]
        public bool DeleteDemandStatusType(int id)
        {
            return _demandService.DeleteDemandStatusType(id);
        }
        #endregion DemandStatusType

        #region DemandComment

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllDemandComments")]
        public List<DemandComment> GetAllDemandComments(int index, int size)
        {
            return _demandService.GetAllDemandComments(index, size).Items as List<DemandComment>;
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddDemandComment")]
        public bool AddDemandComment(DemandComment demandComment)
        {
            return _demandService.AddDemandComment(demandComment);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateDemandComment")]
        public bool UpdateDemandComment(DemandComment demandComment)
        {
            return _demandService.UpdateDemandComment(demandComment);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetDemandComment")]
        public DemandComment GetDemandComment(int id)
        {
            return _demandService.GetDemandComment(id);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteDemandComment")]
        public bool DeleteDemandComment(int id)
        {
            return _demandService.DeleteDemandComment(id);
        }
        #endregion DemandComment

        #region MetaData
        [HttpPost("GetDemandMetaData")]
        [AllowAnonymous]
        public DemandMetaDataResMdl GetDemandMetaData()
        {
            DemandMetaDataResMdl demandMetaDataResMdl = new DemandMetaDataResMdl();

            // Demand Types
            List<DemandType> demandTypes = _demandService.GetAllDemandTypesList();
            UeDemandType ueDemandType;
            foreach (var rec in demandTypes)
            {

                ueDemandType = _mapper.Map<UeDemandType>(rec);
                ueDemandType.LanguageIdTexts = _localizationService.GetAllIdTexts(rec.LocalKey);
                demandMetaDataResMdl.UeDemandTypeList.Add(ueDemandType);
            }

            // Demand Status Types
            List<DemandStatusType> demandStatusTypes = _demandService.GetAllDemandStatusTypesList();
            UeDemandStatusType ueDemandStatusType;
            foreach (var rec in demandStatusTypes)
            {

                ueDemandStatusType = _mapper.Map<UeDemandStatusType>(rec);
                ueDemandStatusType.LanguageIdTexts = _localizationService.GetAllIdTexts(rec.LocalKey);
                demandMetaDataResMdl.UeDemandStatusTypeList.Add(ueDemandStatusType);
            }

            return demandMetaDataResMdl;
        }
        #endregion MetaData
    }
}
