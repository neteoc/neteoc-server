using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NetEOC.Auth.Models;

namespace NetEOC.Auth.Controllers
{
    [Route("auth/users")]
    public class UsersController : Controller
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        [Authorize]
        public Task<ActionResult> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody]User user)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        [Authorize]
        public void Put(Guid id, [FromBody]User user)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
