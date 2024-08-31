using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OAK.Model.ApiModels.RequestMdl;
using OAK.Model.ApiModels.ResultMdl;
using OAK.Model.BusinessModels.AddressModels;
using OAK.Model.BusinessModels.TransportationModels;
using OAK.Model.BusinessModels.TransportationModels.TransportationCalculationModels;
using OAK.Model.Localization;
using OAK.Model.ResultModels;
using OAK.Model.StaticModels;
using OAK.ServiceContracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace OAK.WebApi.Controllers
{


    [Route("api/{culture}/Transportation")]
    [ApiController]

    public class TransportationController : ControllerBase
    {
        private readonly IStringLocalizer<ValuesController> _stringLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly ITransportationService _transportationService;
        private readonly IGenericAddressService _genericAddressService;
        private readonly IEstateService _estateService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _dataDocumentPath;
        private readonly IConfiguration _configuration;


        public TransportationController(ITransportationService transportationService,
            IStringLocalizer<ValuesController> valuesLocalizerizer,
            IStringLocalizer<SharedResource> sharedLocalizer,
            ILogger<TransportationController> logger, IMapper mapper, IGenericAddressService genericAddressService,
            IEstateService estateService,
            IConfiguration _configuration)
        {
            _transportationService = transportationService;
            _stringLocalizer = valuesLocalizerizer;
            _sharedLocalizer = sharedLocalizer;
            _logger = logger;
            _mapper = mapper;
            _genericAddressService = genericAddressService;
            _estateService = estateService;
            _dataDocumentPath = _configuration.GetSection("DocumentSettings:DataDocumentPath").Value;
        }

        [AllowAnonymous]
        [HttpPost("GetTransportationMapData")]
        public TransMapDataResMdl GetTransportationMapData()
        {
            TransMapDataResMdl transMapDataRes = new TransMapDataResMdl();
            try
            {


                using (StreamReader r =
                    new StreamReader(_dataDocumentPath + "assets/data/Features.geojson"))
                {
                    transMapDataRes.geopolygons = r.ReadToEnd();
                }

            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }

            return transMapDataRes;
        }

        #region Transportation
        [Route("InsertTransportationDemand")]
        public string InsertTransportationDemand()
        {
            return "inserted";
        }

        [Route("GetTransportationDemand")]
        public string GetTransportationDemand()
        {
            return "inserted";
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllTransportations")]
        public List<Transportation> GetAllTransportations(int index, int size)
        {

            List<Transportation> transportationList = _transportationService.GetAllTransportations(index, size).Items as List<Transportation>;
            return transportationList;

        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllTransportations")]
        public IActionResult GetAllTransportations([FromBody] TransportationReqMdl transportationReqMdl)
        {
            var transportations = _transportationService.GetAllTransportations(transportationReqMdl.Transportation.TransportationStatusTypeId);
            return Ok(transportations);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllTransportationsWithAddressesAndEstates")]
        public IActionResult GetAllTransportationsWithAddressesAndEstates([FromBody] TransportationReqMdl transportationReqMdl)
        {
            var transportations = _transportationService.GetAllTransportationsWithAddressesAndEstates(transportationReqMdl.Transportation.TransportationStatusTypeId);
            return Ok(transportations);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddTransportation")]
        public bool AddTransportation(Transportation transportation)
        {

            return _transportationService.AddTransportation(transportation);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateTransportation")]
        public IActionResult UpdateTransportation(Transportation transportation)
        {
            var updateTransportation = _transportationService.UpdateTransportation(transportation);
            return Ok(updateTransportation);
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetTransportation")]
        public Transportation GetTransportation(int id)
        {
            return _transportationService.GetTransportation(id);
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteTransportation")]
        public bool DeleteTransportation(int id)
        {
            return _transportationService.DeleteTransportation(id);
        }

        #endregion Transportation

        #region TransportationType

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllTransportationTypes")]
        public List<TransportationType> GetAllTransportationTypes(int index, int size)
        {
            return _transportationService.GetAllTransportationTypes(index, size).Items as List<TransportationType>;
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddTransportationType")]
        public bool AddTransportationType(TransportationTypeReqMdl transportationTypeAddReqMdl)
        {
            return _transportationService.AddTransportationType(transportationTypeAddReqMdl.TransportationType, transportationTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateTransportationType")]
        public bool UpdateTransportationType(TransportationTypeReqMdl transportationTypeAddReqMdl)
        {
            return _transportationService.UpdateTransportationType(transportationTypeAddReqMdl.TransportationType, transportationTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetTransportationType")]
        public TransportationType GetTransportationType(int id)
        {
            return _transportationService.GetTransportationType(id);

        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteTransportationType")]
        public bool DeleteTransportationType(int id)
        {
            return _transportationService.DeleteTransportationType(id);

        }
        #endregion TransportationType

        #region TransportationStatusType

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllTransportationStatusTypes")]
        public List<TransportationStatusType> GetAllTransportationStatusTypes(int index, int size)
        {
            return _transportationService.GetAllTransportationStatusTypes(index, size).Items as List<TransportationStatusType>;
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddTransportationStatusType")]
        public bool AddTransportationStatusType(TransportationStatusTypeReqMdl transportationStatusTypeAddReqMdl)
        {
            return _transportationService.AddTransportationStatusType(transportationStatusTypeAddReqMdl.TransportationStatusType, transportationStatusTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateTransportationStatusType")]
        public bool UpdateTransportationStatusType(TransportationStatusTypeReqMdl transportationStatusTypeAddReqMdl)
        {
            return _transportationService.UpdateTransportationStatusType(transportationStatusTypeAddReqMdl.TransportationStatusType, transportationStatusTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetTransportationStatusType")]
        public TransportationStatusType GetTransportationStatusType(int id)
        {
            return _transportationService.GetTransportationStatusType(id);

        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteTransportationStatusType")]
        public bool DeleteTransportationStatusType(int id)
        {
            return _transportationService.DeleteTransportationStatusType(id);

        }
        #endregion TransportationStatusType

        #region TransportationComment

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllTransportationComments")]
        public List<TransportationComment> GetAllTransportationComments(int index, int size)
        {
            return _transportationService.GetAllTransportationComments(index, size).Items as List<TransportationComment>;
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddTransportationComment")]
        public bool AddTransportationComment(TransportationComment transportationComment)
        {
            return _transportationService.AddTransportationComment(transportationComment);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateTransportationComment")]
        public bool UpdateTransportationComment(TransportationComment transportationComment)
        {
            return _transportationService.UpdateTransportationComment(transportationComment);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetTransportationComment")]
        public TransportationComment GetTransportationComment(int id)
        {
            return _transportationService.GetTransportationComment(id);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteTransportationComment")]
        public bool DeleteTransportationComment(int id)
        {
            return _transportationService.DeleteTransportationComment(id);
        }
        #endregion Transportation

        #region TransportationDocument

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllTransportationDocuments")]
        public List<TransportationDocument> GetAllTransportationDocuments(int index, int size)
        {
            return _transportationService.GetAllTransportationDocuments(index, size).Items as List<TransportationDocument>;
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddTransportationDocument")]
        public bool AddTransportationDocument(TransportationDocument transportationDocument)
        {
            return _transportationService.AddTransportationDocument(transportationDocument);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateTransportationDocument")]
        public bool UpdateTransportationDocument(TransportationDocument transportationDocument)
        {
            return _transportationService.UpdateTransportationDocument(transportationDocument);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetTransportationDocument")]
        public TransportationDocument GetTransportationDocument(int id)
        {
            return _transportationService.GetTransportationDocument(id);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteTransportationDocument")]
        public bool DeleteTransportationDocument(int id)
        {
            return _transportationService.DeleteTransportationDocument(id);
        }
        #endregion TransportationDocument

        #region TransportationCostCalculation
        // [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [AllowAnonymous]
        [HttpPost("CalculateTransportationCostML")]
        public TransCalResMdl CalculateTransportationCostML([FromBody] TransCalReq calculationVariables)
        {
            TransCalResMdl transCalResMdl = new TransCalResMdl()
            {
                resultBaseMdl = new ResultBaseModel()
                {
                    IsValid = true,
                    StatusCode = 0
                }
            };
            try
            {
                transCalResMdl.transCalRes = _transportationService.CalculateTransportationCostML(calculationVariables);
            }
            catch
            {
                transCalResMdl.resultBaseMdl.IsValid = false;
            }

            return transCalResMdl;
        }
        #endregion TransportationCostCalculation



    }
}
