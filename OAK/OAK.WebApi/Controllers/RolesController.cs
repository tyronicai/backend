using Microsoft.AspNetCore.Cors;

namespace OAK.WebApi.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using OAK.Model.Core;
    using OAK.ServiceContracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/{culture}/Roles")]

    public class RolesController : ControllerBase
    {
        private readonly IRoleService RoleService;
        private readonly ILogger Logger;
        private readonly IMapper _mapper;
        public RolesController(IRoleService roleService, ILogger<RolesController> logger, IMapper mapper)
        {
            RoleService = roleService;
            Logger = logger;
            _mapper = mapper;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Role role)
        {
            RoleService.Add(role);
            return Ok();
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] Role role)
        {
            RoleService.Update(role);
            return Ok();
        }
    }
}
