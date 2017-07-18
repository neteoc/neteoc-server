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
    [Route("flaregroups")]
    public class FlareGroupsController : BaseController
    {
        public FlareGroupService FlareGroupService { get; set; }

        public FlareGroupsController()
        {
            FlareGroupService = new FlareGroupService();
        }

        [Authorize, HttpGet]
        [SwaggerResponse(typeof(FlareGroup[]), Description = "Get all flare groups for the currently logged in user. This includes both ownerships and memberships.")]
        public async Task<ActionResult> Get()
        {
            Guid userId = GetUserIdFromContext();

            return Ok(await FlareGroupService.GetFlareGroupsByUserId(userId));
        }

        [Authorize, HttpGet("{id}")]
        [SwaggerResponse(typeof(FlareGroup), Description = "Get the flare group with the given id.")]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(await FlareGroupService.GetFlareGroupById(id));
        }

        [Authorize, HttpPost]
        [SwaggerResponse(typeof(FlareGroup), Description = "Create a new flare group.")]
        public async Task<ActionResult> Post([FromBody] FlareGroup flareGroup)
        {
            return Ok(await FlareGroupService.CreateFlareGroup(flareGroup));
        }

        [Authorize, HttpPut("{id}")]
        [SwaggerResponse(typeof(FlareGroup), Description = "Update the flare group with the given id.")]
        public async Task<ActionResult> Put(Guid id, [FromBody] FlareGroup flareGroup)
        {
            flareGroup.Id = id;
            return Ok(await FlareGroupService.UpdateFlareGroup(flareGroup));
        }

        [Authorize, HttpDelete("{id}")]
        [SwaggerResponse(typeof(bool), Description = "Delete the flare group with the given id.")]
        public async Task<ActionResult> Delete(Guid id)
        {
            return Ok(await FlareGroupService.DeleteFlareGroup(id));
        }

        [Authorize, HttpPost("{flareGroupId}/members/{userId}")]
        [SwaggerResponse(typeof(FlareGroupMember), Description = "Make a user a member of the given flare group.")]
        public async Task<ActionResult> AddMemberToFlareGroup(Guid flareGroupId, Guid userId)
        {
            return Ok(await FlareGroupService.AddMemberToFlareGroup(flareGroupId, userId));
        }

        [Authorize, HttpDelete("{flareGroupId}/members/{userId}")]
        [SwaggerResponse(typeof(bool), Description = "Remove a user from the members of a flare group.")]
        public async Task<ActionResult> RemoveMemberFromFlareGroup(Guid flareGroupId, Guid userId)
        {
            return Ok(await FlareGroupService.RemoveMemberFromFlareGroup(flareGroupId, userId));
        }

        [Authorize, HttpGet("{id}/members")]
        [SwaggerResponse(typeof(Guid[]), Description = "Get all the members of the given flare group. This only returns the member ids.")]
        public async Task<ActionResult> GetFlareGroupMembers(Guid flareGroupId)
        {
            return Ok(await FlareGroupService.GetFlareGroupMembers(flareGroupId));
        }

    }
}
