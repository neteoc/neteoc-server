using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetEOC.Flare.Models;
using NetEOC.Flare.Data;
using Microsoft.AspNetCore.Authorization;
using NSwag.Annotations;

namespace NetEOC.Flare.Controllers
{
    [Route("flares")]
    public class FlaresController : BaseController
    {
        FlareRepository repo = new FlareRepository();

        [Authorize, HttpGet]
        [SwaggerResponse(typeof(Models.Flare[]), Description = "Get all flares that the currently logged in user has created.")]
        public async Task<ActionResult> Get()
        {
            return Ok(new[] { new Models.Flare() });
        }
    }
}
