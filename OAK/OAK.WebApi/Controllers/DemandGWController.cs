using Microsoft.AspNetCore.Cors;
using OAK.Model.BusinessModels.CompanyModels;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

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
    using OAK.Model.BusinessModels.EstateModels;
    using OAK.Model.BusinessModels.TransportationModels;
    using OAK.Model.Localization;
    using OAK.Model.StaticModels;
    using OAK.Model.ViewModels.DemandModels;
    using OAK.Model.ViewModels.TransportationModels;
    using OAK.ServiceContracts;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    [Route("api/{culture}/DemandGW")]
    [ApiController]
    public class DemandGWController : ControllerBase
    {
        private readonly IStringLocalizer<ValuesController> _stringLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IDemandService _demandService;
        private readonly ITransportationService _transportationService;
        private readonly IEstateService _estateService;
        private readonly IGenericAddressService _genericAddressService;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        private readonly ILogger Logger;
        //ILanguageService

        public DemandGWController(IDemandService demandService,
            ITransportationService transportationService,
            IEstateService estateService,
            IGenericAddressService genericAddressService,
            ILocalizationService localizationService,
            IStringLocalizer<ValuesController> valuesLocalizerizer,
            IStringLocalizer<SharedResource> sharedLocalizer,
            ILogger<DemandGWController> logger, IMapper mapper)
        {
            _demandService = demandService;
            _transportationService = transportationService;
            _estateService = estateService;
            _stringLocalizer = valuesLocalizerizer;
            _sharedLocalizer = sharedLocalizer;
            _localizationService = localizationService;
            _genericAddressService = genericAddressService;
            Logger = logger;
            _mapper = mapper;
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("CreateTransportationDemand")]
        async public Task<CreateTransportationDemandResMdl> CreateTransportationDemand(CreateTransportationDemandReqMdl createTransportationDemandReqMdl)
        {
            Claim y = Request.HttpContext.User.Claims.First();

            CreateTransportationDemandResMdl createTransportationDemandResMdl = new CreateTransportationDemandResMdl();
            Demand demand = _mapper.Map<Demand>(createTransportationDemandReqMdl.Demand);
            demand.AccountId = Convert.ToInt32(Request.HttpContext.User.Claims.First().Value);
            Transportation transportation = _mapper.Map<Transportation>(createTransportationDemandReqMdl.Transportation);

            demand.DemandStatusTypeId = 2;

            if (demand.Id == 0)
            {
                _demandService.AddDemand(demand);
                /*
                if (null != demand.DemandOwner)
                {
                    Demand updDemand = _demandService.GetDemand(demand.Id);
                    updDemand.AccountId = demand.AccountId;
                    updDemand.DemandOwnerId = demand.DemandOwner.Id;
                    _demandService.UpdateDemand(updDemand);
                }
                */
            }
            else
            {
                _demandService.UpdateDemand(demand);
            }

            transportation.DemandId = demand.Id;
            _genericAddressService.AddGenericAddress(transportation.FromAddress, null);
            _genericAddressService.AddGenericAddress(transportation.ToAddress, null);
            transportation.FromAddressId = transportation.FromAddress.Id;
            transportation.ToAddressId = transportation.ToAddress.Id;


            if (null != transportation.FromEstate.Flats)
            {

                foreach (var rFlat in transportation.FromEstate.Flats.ToList())
                {
                    if (null == rFlat)
                    {
                        transportation.FromEstate.Flats.Remove(rFlat);
                        continue;
                    }

                    foreach (var rEstatePart in rFlat.EstateParts.ToList())
                    {
                        if (null == rEstatePart)
                        {
                            rFlat.EstateParts.Remove(rEstatePart);
                            continue;
                        }

                        foreach (var rFurniture in rEstatePart.Furnitures.ToList())
                        {
                            if (null == rFurniture)
                            {
                                rEstatePart.Furnitures.Remove(rFurniture);
                                continue;
                            }

                            if (0 == rFurniture.NumberOfFurnitures)
                            {
                                rEstatePart.Furnitures.Remove(rFurniture);
                                continue;
                            }

                        }

                        if (0 == rEstatePart.Furnitures.Count)
                        {
                            rFlat.EstateParts.Remove(rEstatePart);
                            continue;
                        }
                    }

                    if (0 == transportation.FromEstate.Flats.Count)
                    {
                        transportation.FromEstate.Flats.Remove(rFlat);
                        continue;
                    }
                }
            }

            _estateService.AddEstate(transportation.FromEstate);
            _estateService.AddEstate(transportation.ToEstate);

            Transportation rTransportation = _mapper.Map<Transportation>(transportation);

            _transportationService.AddTransportation(rTransportation);
            //if (transportation.Id == 0)
            //{
            //    _transportationService.AddTransportation(transportation);
            //}
            //else
            //{
            //    _transportationService.UpdateTransportation(transportation);
            //}

            createTransportationDemandResMdl.Demand = _mapper.Map<UeDemand>(demand);
            createTransportationDemandResMdl.Transportation = _mapper.Map<UeTransportation>(transportation);
            // await _reportService.SendDemandReportToCustomer(demand, transportation);
            return createTransportationDemandResMdl;

        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AcceptOffer")]
        public IActionResult AcceptOffer([FromBody] AcceptOfferReqMdl acceptOfferReqMdl)
        {
            var result = _demandService.AcceptOffer(acceptOfferReqMdl);
            return Ok(result);
        }
    }
}
