using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetEOC.Flare.Models;
using NetEOC.Flare.Services;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Controllers
{
    [Route("users")]
    public class UsersController : BaseController
    {
        public FlareGroupService FlareGroupService { get; set; }

        public FlareService FlareService { get; set; }

        public UsersController()
        {
            FlareGroupService = new FlareGroupService();

            FlareService = new FlareService();
        }

        [Authorize, HttpGet("{id}/flaregroups")]
        [SwaggerResponse(typeof(FlareGroup[]), Description = "Get all flare groups that a user is currently associated with. This includes both ownerships and memberships.")]
        public async Task<ActionResult> GetUserFlareGroups(Guid id)
        {
            return Ok(await FlareGroupService.GetFlareGroupsByUserId(id));
        }

        [Authorize, HttpGet("{id}/flares")]
        [SwaggerResponse(typeof(Models.Flare[]), Description = "Get all flares sent by the given user.")]
        public async Task<ActionResult> GetUserFlares(Guid id)
        {
            return Ok(await FlareService.GetFlaresBySenderId(id));
        }
    }
}
