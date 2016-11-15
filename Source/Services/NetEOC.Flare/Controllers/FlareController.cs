using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetEOC.Flare.Models;
using NetEOC.Flare.Data;
using Microsoft.AspNetCore.Authorization;

namespace NetEOC.Flare.Controllers
{
    [Route("flare")]
    public class FlareController : Controller
    {
        FlareRepository repo = new FlareRepository();

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            return Ok(new Models.Flare());
        }

        [HttpGet("{id}")]
        [Authorize]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody]Models.Flare flare)
        {
            Models.Flare result = await repo.Create(flare);

            if (flare != null) return Ok(flare);

            return StatusCode(500);
        }

        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(int id)
        {
        }
    }
}
