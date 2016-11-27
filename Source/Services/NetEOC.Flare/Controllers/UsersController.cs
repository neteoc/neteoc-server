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

        public UsersController()
        {
            FlareGroupService = new FlareGroupService();
        }

        [Authorize, HttpGet("{id}/flaregroups")]
        [SwaggerResponse(typeof(FlareGroup[]), Description = "Get all flare groups that a user is currently associated with. This includes both ownerships and memberships.")]
        public async Task<ActionResult> GetUserFlareGroups(Guid id)
        {
            return Ok(await FlareGroupService.GetFlareGroupsByUserId(id));
        }
    }
}
