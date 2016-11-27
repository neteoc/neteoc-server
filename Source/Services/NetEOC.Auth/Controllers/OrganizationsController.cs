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
        [SwaggerResponse(typeof(Organization))]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(await OrganizationService.GetById(id));
        }

        [Authorize, HttpPost]
        [SwaggerResponse(typeof(Organization))]
        public async Task<ActionResult> Post([FromBody] Organization organization)
        {
            return Ok(await OrganizationService.Create(organization));
        }

        [Authorize, HttpPut("{id}")]
        [SwaggerResponse(typeof(Organization))]
        public async Task<ActionResult> Put(Guid id, [FromBody] Organization organization)
        {
            return Ok(await OrganizationService.Update(organization));
        }

        [Authorize, HttpGet("{id}/members")]
        [SwaggerResponse(typeof(Guid[]))]
        public async Task<ActionResult> Members(Guid id)
        {
            return Ok(await OrganizationService.GetOrganizationMembers(id));
        }

        [Authorize, HttpGet("{id}/administrators")]
        [SwaggerResponse(typeof(Guid[]))]
        public async Task<ActionResult> Administrators(Guid id)
        {
            return Ok(await OrganizationService.GetOrganizationAdmins(id));
        }

        [Authorize, HttpPost("{id}/members/{userId}")]
        [SwaggerResponse(typeof(OrganizationMember))]
        public async Task<ActionResult> AddMember(Guid id, Guid userId)
        {
            return Ok(await OrganizationService.AddMemberToOrganization(id, userId));
        }

        [Authorize, HttpPost("{id}/administrators/{userId}")]
        [SwaggerResponse(typeof(OrganizationAdmin))]
        public async Task<ActionResult> AddAdministrator(Guid id, Guid userId)
        {
            return Ok(await OrganizationService.AddAdminToOrganization(id, userId));
        }

        [Authorize, HttpDelete("{id}/members/{userId}")]
        [SwaggerResponse(typeof(bool))]
        public async Task<ActionResult> RemoveMember(Guid id, Guid userId)
        {
            return Ok(await OrganizationService.RemoveMemberFromOrganization(id, userId));   
        }

        [Authorize, HttpDelete("{id}/administrators/{userId}")]
        [SwaggerResponse(typeof(bool))]
        public async Task<ActionResult> RemoveAdministrator(Guid id, Guid userId)
        {
            return Ok(await OrganizationService.RemoveAdminFromOrganization(id, userId));
        }
    }
}
