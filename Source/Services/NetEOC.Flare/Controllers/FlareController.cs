using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetEOC.Flare.Models;
using NetEOC.Flare.Data;

namespace NetEOC.Flare.Controllers
{
    [Route("flare")]
    public class FlareController : Controller
    {
        FlareRepository repo = new FlareRepository();

        // GET api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(new Models.Flare());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Models.Flare flare)
        {
            Models.Flare result = await repo.Create(flare);

            if (flare != null) return Ok(flare);

            return StatusCode(500);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
