using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NetEOC.Auth.Models;
using NetEOC.Auth.Services;

namespace NetEOC.Auth.Controllers
{
    [Route("auth/users")]
    public class UsersController : Controller
    {
        public UserService UserService { get; set; }

        public UsersController()
        {
            UserService = new UserService();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            string authId = GetAuthIdFromContext();

            User user = await UserService.GetByAuthId(authId);

            return Ok(user);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(await UserService.GetById(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody]User user)
        {
            user.AuthId = GetAuthIdFromContext();

            if (user.Id != Guid.Empty)
            {
                user = await UserService.Update(user);

                return Ok(user);
            }

            User existing = await UserService.GetByAuthId(user.AuthId);

            if(existing != null)
            {
                user.Id = existing.Id;

                user = await UserService.Update(user);

                return Ok(user);
            }

            user = await UserService.Create(user);

            return Ok(user);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Put(Guid id, [FromBody]User user)
        {
            user.AuthId = GetAuthIdFromContext();

            bool canUpdate = await UserService.Validate(user.AuthId, id);

            if (canUpdate)
            {
                user.Id = id;

                user = await UserService.Update(user);

                return Ok(user);
            }

            return StatusCode(401); //the current user isnt authorized to update this user
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(Guid id)
        {
            bool canDelete = await UserService.Validate(GetAuthIdFromContext(), id);

            if (canDelete)
            {
                bool deleted = await UserService.Delete(id);

                return Ok(deleted);
            }

            return StatusCode(401); //the current user isnt authorized to update this user
        }

        [HttpGet("{id}/organizations")]
        [Authorize]
        public async Task<ActionResult> Organizations(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}/valid")]
        [Authorize]
        public async Task<ActionResult> ValidateAuthId(Guid id)
        {
            string authId = GetAuthIdFromContext();

            bool valid = await UserService.Validate(authId, id);

            return Ok(valid);
        }

        [Authorize]
        [HttpGet("claims")]
        public object Claims()
        {
            return User.Claims.Select(c =>
            new
            {
                Type = c.Type,
                Value = c.Value
            });
        }

        private string GetAuthIdFromContext()
        {
            return User.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
        }
    }
}
