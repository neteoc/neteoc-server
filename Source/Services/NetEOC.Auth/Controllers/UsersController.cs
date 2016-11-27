using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NetEOC.Auth.Models;
using NetEOC.Auth.Services;
using NetEOC.Auth.Integrations.Auth0;
using NSwag.Annotations;

namespace NetEOC.Auth.Controllers
{
    [Route("users")]
    public class UsersController : BaseController
    {
        public UserService UserService { get; set; }

        public OrganizationService OrganizationService { get; set; }

        public Auth0UserService Auth0UserService { get; set; }

        public UsersController()
        {
            UserService = new UserService();

            OrganizationService = new OrganizationService();

            Auth0UserService = new Auth0UserService();
        }

        [Authorize, HttpGet]
        [SwaggerResponse(typeof(User))]
        public async Task<ActionResult> Get()
        {
            string authId = GetAuthIdFromContext();

            User user = await UserService.GetByAuthId(authId);

            return Ok(user);
        }

        [Authorize, HttpGet("{id}")]
        [SwaggerResponse(typeof(User))]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(await UserService.GetById(id));
        }

        [Authorize, HttpPost]
        [SwaggerResponse(typeof(User))]
        public async Task<ActionResult> Post([FromBody]User user)
        {
            user.AuthId = GetAuthIdFromContext();

            User existing = await UserService.GetByAuthId(user.AuthId);

            if(existing != null)
            {
                user.Id = existing.Id;

                user = await UserService.Update(user);

                Guid claimUserId = GetUserIdFromContext();

                if(claimUserId == Guid.Empty || claimUserId != existing.Id)
                {
                    bool updateAuth0IdSuccess = await Auth0UserService.SetUserLocalId(user.AuthId, existing.Id.ToString());

                    return StatusCode(401); //we return not authorized to tell the front end to invalidate the jwt
                }

                return Ok(user);
            }

            user = await UserService.Create(user);

            bool createAuth0IdSuccess = await Auth0UserService.SetUserLocalId(user.AuthId, user.Id.ToString());

            return StatusCode(401); // we return not authorized to tell the front end to invalidate the jwt
        }

        [Authorize, HttpPut("{id}")]
        [SwaggerResponse(typeof(User))]
        public async Task<ActionResult> Put(Guid id, [FromBody]User user)
        {
            user.AuthId = GetAuthIdFromContext();

            bool canUpdate = id == GetUserIdFromContext();

            if (canUpdate)
            {
                user.Id = id;

                user = await UserService.Update(user);

                return Ok(user);
            }

            return StatusCode(401); //the current user isnt authorized to update this user
        }

        [Authorize, HttpDelete("{id}")]
        [SwaggerResponse(typeof(bool))]
        public async Task<ActionResult> Delete(Guid id)
        {
            bool canDelete = id == GetUserIdFromContext();

            if (canDelete)
            {
                bool deleted = await UserService.Delete(id);

                return Ok(deleted);
            }

            return StatusCode(401); //the current user isnt authorized to update this user
        }

        [Authorize, HttpGet("{id}/organizations")]
        [SwaggerResponse(typeof(Organization[]))]
        public async Task<ActionResult> Organizations(Guid id)
        {
            Guid[] orgs = await UserService.GetUserOrganizations(id);

            return Ok(await Task.WhenAll(orgs.Select(OrganizationService.GetById)));
        }
    }
}
