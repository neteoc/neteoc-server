using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetEOC.Auth.Models;
using NetEOC.Auth.Services;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Auth.Controllers
{
    [Route("organizations")]
    public class OrganizationsController : BaseController
    {
        public UserService UserService { get; set; }

        public OrganizationService OrganizationService { get; set; }

        public OrganizationsController()
        {
            UserService = new UserService();

            OrganizationService = new OrganizationService();
        }

        [Authorize, HttpGet("{id}")]
        [SwaggerResponse(typeof(Organization), Description = "Get the organization with the given id.")]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(await OrganizationService.GetById(id));
        }

        [Authorize, HttpPost]
        [SwaggerResponse(typeof(Organization), Description = "Create a new organization.")]
        public async Task<ActionResult> Post([FromBody] Organization organization)
        {
            return Ok(await OrganizationService.Create(organization));
        }

        [Authorize, HttpPut("{id}")]
        [SwaggerResponse(typeof(Organization), Description = "Update the organization with the given id. Not every value is necessary, the provided data is merged into the existing organization.")]
        public async Task<ActionResult> Put(Guid id, [FromBody] Organization organization)
        {
            return Ok(await OrganizationService.Update(organization));
        }

        [Authorize, HttpGet("{id}/members")]
        [SwaggerResponse(typeof(Guid[]), Description = "Get all members belonging to the given organization. These are just the member ids.")]
        public async Task<ActionResult> Members(Guid id)
        {
            return Ok(await OrganizationService.GetOrganizationMembers(id));
        }

        [Authorize, HttpGet("{id}/administrators")]
        [SwaggerResponse(typeof(Guid[]), Description = "Get all members that are an administrator of a given organization. These are just the member ids.")]
        public async Task<ActionResult> Administrators(Guid id)
        {
            return Ok(await OrganizationService.GetOrganizationAdmins(id));
        }

        [Authorize, HttpPost("{id}/members/{userId}")]
        [SwaggerResponse(typeof(OrganizationMember), Description = "Add a member to an organization.")]
        public async Task<ActionResult> AddMember(Guid id, Guid userId)
        {
            return Ok(await OrganizationService.AddMemberToOrganization(id, userId));
        }

        [Authorize, HttpPost("{id}/administrators/{userId}")]
        [SwaggerResponse(typeof(OrganizationAdmin), Description = "Add an administrator to an organization.")]
        public async Task<ActionResult> AddAdministrator(Guid id, Guid userId)
        {
            return Ok(await OrganizationService.AddAdminToOrganization(id, userId));
        }

        [Authorize, HttpDelete("{id}/members/{userId}")]
        [SwaggerResponse(typeof(bool), Description = "Remove a member from an organization.")]
        public async Task<ActionResult> RemoveMember(Guid id, Guid userId)
        {
            return Ok(await OrganizationService.RemoveMemberFromOrganization(id, userId));   
        }

        [Authorize, HttpDelete("{id}/administrators/{userId}")]
        [SwaggerResponse(typeof(bool), Description = "Remove an administrator from an organization.")]
        public async Task<ActionResult> RemoveAdministrator(Guid id, Guid userId)
        {
            return Ok(await OrganizationService.RemoveAdminFromOrganization(id, userId));
        }
    }
}
