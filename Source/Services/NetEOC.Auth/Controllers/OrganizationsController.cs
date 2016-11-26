using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetEOC.Auth.Models;
using NetEOC.Auth.Services;
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


        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(await OrganizationService.GetById(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] Organization organization)
        {
            return Ok(await OrganizationService.Create(organization));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Put(Guid id, [FromBody] Organization organization)
        {
            return Ok(await OrganizationService.Update(organization));
        }

        [HttpGet("{id}/members")]
        public async Task<ActionResult> Members(Guid id)
        {
            return Ok(await OrganizationService.GetOrganizationMembers(id));
        }

        [HttpGet("{id}/administrators")]
        public async Task<ActionResult> Administrators(Guid id)
        {
            return Ok(await OrganizationService.GetOrganizationAdmins(id));
        }

        [HttpPost("{id}/members")]
        public async Task<ActionResult> AddMember(Guid id, [FromBody] SingleIdViewModel model)
        {
            return Ok(await OrganizationService.AddMemberToOrganization(id, model.Id));
        }

        [HttpPost("{id}/administrators")]
        public async Task<ActionResult> AddAdministrator(Guid id, [FromBody] SingleIdViewModel model)
        {
            return Ok(await OrganizationService.AddAdminToOrganization(id, model.Id));
        }

        [HttpDelete("{id}/members/{userId}")]
        public async Task<ActionResult> RemoveMember(Guid id, Guid userId)
        {
            return Ok(await OrganizationService.RemoveMemberFromOrganization(id, userId));   
        }

        [HttpDelete("{id}/administrators/{userId}")]
        public async Task<ActionResult> RemoveAdministrator(Guid id, Guid userId)
        {
            return Ok(await OrganizationService.RemoveAdminFromOrganization(id, userId));
        }
    }
}
