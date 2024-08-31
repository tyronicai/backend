using Microsoft.AspNetCore.Cors;

namespace OAK.WebApi.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using OAK.Model.BusinessModels.ParameterModels;
    using OAK.Model.Localization;
    using OAK.Model.StaticModels;
    using OAK.ServiceContracts;
    using System.Collections.Generic;
    using System.Globalization;

    [Route("api/{culture}/Parameters")]
    [ApiController]
    public class ParameterController : ControllerBase
    {
        private readonly IStringLocalizer<ValuesController> _stringLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IParameterService _parameterService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ParameterController(IParameterService parameterService, IStringLocalizer<ValuesController> valuesLocalizerizer, IStringLocalizer<SharedResource> sharedLocalizer,
            ILogger<ParameterController> logger, IMapper mapper)
        {
            _parameterService = parameterService;
            _stringLocalizer = valuesLocalizerizer;
            _sharedLocalizer = sharedLocalizer;
            _logger = logger;
            _mapper = mapper;
        }



        #region Parameters


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllParameters")]
        List<Parameters> GetAllParameters(int index, int size)
        {

            List<Parameters> transportationList = _parameterService.GetAllParameters(index, size).Items as List<Parameters>;

            return transportationList;

        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddParameters")]
        bool AddParameters(Parameters transportation)
        {

            return _parameterService.AddParameters(transportation);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateParameters")]
        bool UpdateParameters(Parameters transportation)
        {
            bool retVal = true;
            return retVal;
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetParameters")]
        Parameters GetParameters(int id)
        {
            return _parameterService.GetParameters(id);
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteParameters")]
        bool DeleteParameters(int id)
        {
            return _parameterService.DeleteParameters(id);
        }

        #endregion Parameters

        #region CurrencyParameters

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllCurrencyParameters")]
        List<CurrencyParameters> GetAllCurrencyParameters(int index, int size)
        {
            return _parameterService.GetAllCurrencyParameters(index, size).Items as List<CurrencyParameters>;
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddCurrencyParameters")]
        bool AddCurrencyParameters(CurrencyParameters currencyParameter)
        {
            return _parameterService.AddCurrencyParameters(currencyParameter);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateCurrencyParameters")]
        bool UpdateCurrencyParameters(CurrencyParameters currencyParameter)
        {
            return _parameterService.UpdateCurrencyParameters(currencyParameter);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetCurrencyParameters")]
        CurrencyParameters GetCurrencyParameters(int id)
        {
            return _parameterService.GetCurrencyParameters(id);

        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteCurrencyParameters")]
        bool DeleteCurrencyParameters(int id)
        {
            return _parameterService.DeleteCurrencyParameters(id);

        }
        #endregion CurrencyParameters



    }
}
