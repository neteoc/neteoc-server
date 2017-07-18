using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Auth.Controllers
{
    [Route("test")]
    public class TestController : BaseController
    {
        [Authorize, HttpGet()]
        [SwaggerResponse(typeof(bool), Description = "Returns True")]
        public async Task<ActionResult> Get()
        {
            return Ok(true);
        }
    }
}
