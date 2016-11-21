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

            User user = await UserService.GetUserByAuthId(authId);

            return Ok(user);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(await UserService.GetUserById(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody]User user)
        {
            user.AuthId = GetAuthIdFromContext();

            if (user.Id != Guid.Empty)
            {
                user = await UserService.UpdateUser(user);

                return Ok(user);
            }

            User existing = await UserService.GetUserByAuthId(user.AuthId);

            if(existing != null)
            {
                user.Id = existing.Id;

                user = await UserService.UpdateUser(user);

                return Ok(user);
            }

            user = await UserService.CreateUser(user);

            return Ok(user);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Put(Guid id, [FromBody]User user)
        {
            user.AuthId = GetAuthIdFromContext();

            bool canUpdate = await UserService.ValidateUser(user.AuthId, id);

            if (canUpdate)
            {
                user.Id = id;

                user = await UserService.UpdateUser(user);

                return Ok(user);
            }

            return StatusCode(401); //the current user isnt authorized to update this user
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(Guid id)
        {
            bool canDelete = await UserService.ValidateUser(GetAuthIdFromContext(), id);

            if (canDelete)
            {
                bool deleted = await UserService.DeleteUser(id);

                return Ok(deleted);
            }

            return StatusCode(401); //the current user isnt authorized to update this user
        }

        [HttpGet("{id}/valid")]
        [Authorize]
        public async Task<ActionResult> ValidateAuthId(Guid id)
        {
            string authId = GetAuthIdFromContext();

            bool valid = await UserService.ValidateUser(authId, id);

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
