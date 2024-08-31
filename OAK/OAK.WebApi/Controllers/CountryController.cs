using Microsoft.AspNetCore.Cors;

namespace OAK.WebApi.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using OAK.Localizer;
    using OAK.Model.ApiModels.RequestMdl;
    using OAK.Model.ApiModels.ResultMdl;
    using OAK.Model.Core;
    using OAK.Model.ResultModels;
    using OAK.Model.StaticModels;
    using OAK.Model.ViewModels.CoreModels;
    using OAK.ServiceContracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize]

    [Route("api/{culture}/Country")]
    [ApiController]
    public class CountryController : BaseController
    {
        private readonly ICountryService CountryService;
        private readonly ILogger Logger;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ValuesController> _stringLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly ILocalizationService _localizationService;

        public CountryController(ICountryService countryService, ILogger<CountryController> logger, IMapper mapper,
            IStringLocalizer<ValuesController> valuesLocalizerizer, IStringLocalizer<SharedResource> sharedLocalizer,
            ILocalizationService localizationService)
        {
            CountryService = countryService;
            Logger = logger;
            _mapper = mapper;
            _localizationService = localizationService;
            _stringLocalizer = valuesLocalizerizer;
            _sharedLocalizer = sharedLocalizer;
        }


        [HttpGet("getall")]
        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        public IActionResult GetAll(int index, int size)
        {
            var result = CountryService.GetAll(index, size);
            return Ok(result);
        }



        [HttpPost("GetAllCountryList")]
        [AllowAnonymous]
        public CountryListResMdl GetAllCountryList()
        {
            CountryListResMdl countryListResMdl = new CountryListResMdl();
            List<Country> countryList = CountryService.GetAllCountryList();
            foreach (var country in countryList)
            {
                countryListResMdl.UeCountryList.Add(_mapper.Map<Country, UeCountry>(country));
            }

            return countryListResMdl;
        }


        [HttpPost("GetActiveCountryList")]
        [AllowAnonymous]
        public CountryListResMdl GetActiveCountryList()
        {
            CountryListResMdl countryListResMdl = new CountryListResMdl();
            List<Country> countryList = CountryService.GetActiveCountryList();
            foreach (var country in countryList)
            {
                var supportedList = CountryService.GetSupportedPostCodesByCountryList(country.Id);
                if (supportedList.Count > 0)
                {
                    countryListResMdl.UeSupportedPostCodeDataList.AddRange(supportedList);
                }
            }
            UeCountry ueCountry;
            foreach (var country in countryList)
            {
                ueCountry = _mapper.Map<UeCountry>(country);
                ueCountry.LanguageIdTexts = _localizationService.GetAllIdTexts(ueCountry.LocalKey);
                countryListResMdl.UeCountryList.Add(ueCountry);
            }

            return countryListResMdl;
        }



        [HttpPost("GetSupportedPostCodesByCountryList")]
        [AllowAnonymous]
        public List<PostCodeData> GetSupportedPostCodesByCountryList([FromBody] GetPostCodesByDataReqMdl getSupportedPostCodesByCountryListeReqMdl)
        {
            return CountryService.GetSupportedPostCodesByCountryList(getSupportedPostCodesByCountryListeReqMdl.countryId);
        }


        [HttpPost("GetPCDListByCountryIdAndPostCode")]
        [AllowAnonymous]
        public List<PostCodeData> GetPostCodeDataListByCountryIdAndPostCode([FromBody] GetPostCodesByDataReqMdl getPostCodesByDataReqMdl)
        {
            return CountryService.GetPostCodeDataListByCountryIdAndPostCode(getPostCodesByDataReqMdl.countryId, getPostCodesByDataReqMdl.postCodeStr);
        }

        [AllowAnonymous]
        [HttpPost("GetPCDListByCountryIdAndPlaceName")]
        public List<PostCodeData> GetPostCodeDataListByCountryIdAndPlaceName([FromBody] GetPostCodesByDataReqMdl getPostCodesByDataReqMdl)
        {
            return CountryService.GetPostCodeDataListByCountryIdAndPlaceName(getPostCodesByDataReqMdl.countryId, getPostCodesByDataReqMdl.placeNameStr);
        }

        [AllowAnonymous]
        [HttpPost("GetSupportedPostCodes")]
        public List<UeSupportedPostCode> GetSupportedPostCodes()
        {
            List<UeSupportedPostCode> ueSupportedPostCodes = new List<UeSupportedPostCode>();
            List<SupportedPostCode> supportedPostCodes = CountryService.GetSupportedPostCodes();
            foreach (var supportedPostCode in supportedPostCodes)
            {
                ueSupportedPostCodes.Add(_mapper.Map<UeSupportedPostCode>(supportedPostCode));
            }

            return ueSupportedPostCodes;
        }

        [AllowAnonymous]
        [HttpPost("GetCountryById")]
        public IActionResult GetCountryById(GetCountryByIdReqMdl getCountryByIdReqMdl)
        {
            var result = CountryService.Get(getCountryByIdReqMdl.Id);
            return Ok(result);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpGet("get")]
        public IActionResult Get(int id)
        {
            var result = CountryService.Get(id);
            return Ok(result);
        }

        /*
        [HttpPost("add")]
        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        public IActionResult Add([FromBody]Country model)
        {
            bool result = CountryService.Add(model);
            if (result)
                return Ok();
            else
                return NotFound();
        }
        */

        [HttpPost("update")]
        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        public IActionResult Update([FromBody] Country model)
        {
            bool result = CountryService.Update(model);
            if (result)
                return Ok();
            else
                return NotFound();
        }
    }
}
