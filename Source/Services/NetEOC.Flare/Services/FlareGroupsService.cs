using NetEOC.Flare.Data;
using NetEOC.Flare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Services
{
    public class FlareGroupService
    {
        public FlareGroupRepository FlareGroupRepository { get; set; }

        public UserFlareGroupsService UserFlareGroupsService { get; set; }

        public FlareGroupService()
        {
            FlareGroupRepository = new FlareGroupRepository();

            UserFlareGroupsService = new UserFlareGroupsService();
        }

        public async Task<FlareGroup> CreateFlareGroup(FlareGroup flareGroup)
        {
            if (!ValidateFlareGroup(flareGroup)) throw new ArgumentException("Invalid Flare Group!");

            flareGroup.Id = Guid.NewGuid();

            if(flareGroup.Members == null)
            {
                flareGroup.Members = new List<Guid>();

                flareGroup.Members.Add(flareGroup.AdminId);
            }

            flareGroup = await FlareGroupRepository.Create(flareGroup);

            UserFlareGroups userFlareGroups = await UserFlareGroupsService.AddUserToFlareGroup(flareGroup.AdminId, flareGroup.Id);

            return flareGroup;
        }

        public async Task<FlareGroup> GetFlareGroupById(Guid id)
        {
            return await FlareGroupRepository.Get(id);
        }

        public async Task<FlareGroup[]> GetFlareGroupsByOrganizationId(Guid organizationId)
        {
            return await FlareGroupRepository.GetByOrganizationId(organizationId);
        }

        public async Task<FlareGroup> UpdateFlareGroup(FlareGroup flareGroup)
        {
            if (!ValidateFlareGroup(flareGroup)) throw new ArgumentException("Invalid Flare Group!");

            FlareGroup existing = await GetFlareGroupById(flareGroup.Id);

            if (existing == null) throw new ArgumentException("The given flare group does not exist!");

            flareGroup.Members = existing.Members; // we do not currently allow memebers to be updated via the update flare group method

            flareGroup = await FlareGroupRepository.Update(flareGroup);

            if(flareGroup.AdminId != existing.AdminId) //see if the group changed owners, if it did, verify that the new owner is a member
            {
                UserFlareGroups userFlareGroups = await UserFlareGroupsService.AddUserToFlareGroup(flareGroup.AdminId, flareGroup.Id);
            }

            return flareGroup;
        }

        public async Task<bool> DeleteFlareGroup(Guid id)
        {
            FlareGroup flareGroup = await GetFlareGroupById(id);

            if (flareGroup == null) throw new ArgumentException("The given flare group does not exist!");

            //remove membership for group from all memebers
            foreach(Guid member in flareGroup.Members)
            {
                UserFlareGroups userFlareGroups = await UserFlareGroupsService.RemoveUserFromFlareGroup(member, flareGroup.Id);
            }

            return await FlareGroupRepository.Delete(id);
        }

        private bool ValidateFlareGroup(FlareGroup flareGroup)
        {
            if (flareGroup.AdminId == Guid.Empty) throw new ArgumentException("Flare group must have an admin!");

            if (flareGroup.OrganizationId == Guid.Empty) throw new ArgumentException("Flare group must have an organization!");

            if (string.IsNullOrWhiteSpace(flareGroup.Name)) throw new ArgumentException("Flare group must have a name!");

            return true;
        }
    }
}
