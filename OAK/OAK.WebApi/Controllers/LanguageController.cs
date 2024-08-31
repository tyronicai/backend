using Microsoft.AspNetCore.Cors;

namespace OAK.WebApi.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using OAK.Localizer;
    using OAK.Model.ApiModels.ResultMdl;
    using OAK.Model.Core;
    using OAK.Model.StaticModels;
    using OAK.ServiceContracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize]
    [ApiController]
    [Route("api/{culture}/Language")]
    public class LanguageController : BaseController
    {
        private readonly ILanguageService _languageService;
        private readonly ILogger Logger;
        private readonly IStringLocalizer<LanguageController> _stringLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;

        public LanguageController(ILanguageService languageService, ILogger<LanguageController> logger, IStringLocalizer<LanguageController> stringLocalizer, IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper)
        {
            _mapper = mapper;
            _languageService = languageService;
            Logger = logger;
            _stringLocalizer = stringLocalizer;
            _sharedLocalizer = sharedLocalizer;
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpGet("getall")]
        public IActionResult GetAll(int index, int size)
        {
            var items = _languageService.GetAll(index, size);

            return Ok(items);
        }


        [HttpPost("GetAllLanguageList")]
        [AllowAnonymous]
        public LanguageListResMdl GetAllLanguageList(int index, int size)
        {
            LanguageListResMdl languageListResMdl = new LanguageListResMdl();
            List<Language> langList = _languageService.GetAllLanguageList();
            foreach (var language in langList)
            {
                languageListResMdl.UeLanguageList.Add(_mapper.Map<Language, UeLanguage>(language));
            }

            return languageListResMdl;
        }


        [HttpPost("GetActiveLanguageList")]
        [AllowAnonymous]
        public LanguageListResMdl GetActiveLanguageList(int index, int size)
        {
            LanguageListResMdl languageListResMdl = new LanguageListResMdl();
            List<Language> langList = _languageService.GetActiveLanguageList();
            foreach (var language in langList)
            {
                languageListResMdl.UeLanguageList.Add(_mapper.Map<Language, UeLanguage>(language));
            }

            return languageListResMdl;
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("add")]
        public IActionResult Add(Language language)
        {
            var result = _languageService.Add(language);

            if (result)
                return Ok();
            else
                return NotFound();
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("update")]
        public IActionResult Update(Language language)
        {
            var result = _languageService.Update(language);
            if (result)
                return Ok();
            else
                return NotFound();
        }
    }
}
