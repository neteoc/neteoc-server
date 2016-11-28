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
    [Route("organizations")]
    public class OrganizationsController : BaseController
    {
        public FlareGroupService FlareGroupsService { get; set; }

        public FlareService FlareService { get; set; }

        public OrganizationsController()
        {
            FlareGroupsService = new FlareGroupService();

            FlareService = new FlareService();
        }

        [Authorize, HttpGet("{id}/flaregroups")]
        [SwaggerResponse(typeof(FlareGroup[]), Description = "Get all flare groups within the given organization")]
        public async Task<ActionResult> GetOrganizationFlareGroups(Guid id)
        {
            return Ok(await FlareGroupsService.GetFlareGroupsByOrganizationId(id));
        }

        [Authorize, HttpGet("{id}/flares")]
        [SwaggerResponse(typeof(Models.Flare[]), Description = "Get all flares sent within the given organization")]
        public async Task<ActionResult> GetOrganizationFlares(Guid id)
        {
            return Ok(await FlareService.GetFlaresByOrganizationId(id));
        }

    }
}
