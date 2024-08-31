using Microsoft.AspNetCore.Cors;
using OAK.Model.ApiModels.RequestMdl;
using OAK.Model.BusinessModels.AddressModels;

namespace OAK.WebApi.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using OAK.Model.Core;
    using OAK.Model.Localization;
    using OAK.Model.StaticModels;
    using OAK.ServiceContracts;

    [Authorize]
    [ApiController]
    [Route("api/{culture}/GenericAddress")]
    public class GenericAddressController : BaseController
    {
        private readonly IGenericAddressService _genericAddressService;
        private readonly ILogger Logger;
        private readonly IMapper _mapper;

        private readonly IStringLocalizer<GenericAddressController> _stringLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public GenericAddressController(
            IGenericAddressService genericAddressService,
            IMapper mapper, ILogger<GenericAddressController> logger,
            IStringLocalizer<GenericAddressController> valuesLocalizerizer,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _genericAddressService = genericAddressService;
            Logger = logger;
            _stringLocalizer = valuesLocalizerizer;
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpGet("GetAllGenericAddresses")]
        public IActionResult GetAllGenericAddresses()
        {
            var genericAddresses = _genericAddressService.GetAllGenericAddresses();
            return Ok(genericAddresses);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetGenericAddress")]
        public IActionResult GetGenericAddress(GetGenericAddressByIdReqMdl genericAddressByIdReqMdl)
        {
            var address = _genericAddressService.GetGenericAddress(genericAddressByIdReqMdl);
            return Ok(address);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddGenericAddress")]
        public IActionResult AddGenericAddress(GenericAddress genericAddress)
        {
            var address = _genericAddressService.AddGenericAddress(genericAddress);
            return Ok(address);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteGenericAddress")]
        public IActionResult DeleteGenericAddress(int id)
        {
            var address = _genericAddressService.DeleteGenericAddress(id);
            return Ok(address);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateGenericAddress")]
        public IActionResult UpdateGenericAddress(GenericAddress genericAddress)
        {
            var address = _genericAddressService.UpdateGenericAddress(genericAddress);
            return Ok(address);
        }


    }
}