using Microsoft.AspNetCore.Cors;

namespace OAK.WebApi.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using OAK.Model.StaticModels;
    using OAK.ServiceContracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/{culture}/Info")]
    public class InfoController : ControllerBase
    {
        private IControllerDiscoveryService ControllerDiscoveryService;
        private ILogger Logger;
        private readonly IMapper _mapper;

        public InfoController(IControllerDiscoveryService controllerDiscoveryService, ILogger<InfoController> logger, IMapper mapper)
        {
            ControllerDiscoveryService = controllerDiscoveryService;
            Logger = logger;
            _mapper = mapper;
        }

        [HttpGet("getcontrollers")]
        [Authorize]
        public IActionResult GetControllerInfos()
        {
            Logger.LogInformation("GetControllerInfos");

            var result = ControllerDiscoveryService.GetControllers();

            return Ok(result);
        }
    }
}
