using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace SwaggerExtensions.Controllers.V2
{
    //[ApiVersion("2.0")]
    //[SwaggerTag("默认控制器")]
    [Route("api/v2/DefaultV2")]
    [Produces("application/json")]
    //[ApiController]
    public class DefaultV2Controller : ControllerBase
    {
        [HttpGet("Get")]
        public IActionResult Get()
        {
            return new JsonResult("");
        }
    }
}