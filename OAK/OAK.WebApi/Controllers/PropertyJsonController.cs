namespace OAK.WebApi.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using OAK.Model.Localization;
    using System.Globalization;

    [Route("api/{culture}/Property")]
    [ApiController]
    public class PropertyJsonController : ControllerBase
    {
        private readonly IStringLocalizer<ValuesController> _stringLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public PropertyJsonController(IStringLocalizer<ValuesController> valuesLocalizerizer, IStringLocalizer<SharedResource> sharedLocalizer,
            ILogger<PropertyJsonController> logger, IMapper mapper)
        {
            _stringLocalizer = valuesLocalizerizer;
            _sharedLocalizer = sharedLocalizer;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("ShowMeTheCulture")]
        public string GetCulture()
        {
            return $"CurrentCulture:{CultureInfo.CurrentCulture.Name}, CurrentUICulture:{CultureInfo.CurrentUICulture.Name}";
        }

        [Route("InsertTransportationDemand")]
        public string InsertTransportationDemand()
        {
            return "inserted";
        }

        // GET api/values
        [HttpGet]
        public string Get()
        {
            return _sharedLocalizer["ValuesTitleShared"];

            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
