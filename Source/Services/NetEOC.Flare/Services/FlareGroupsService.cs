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

        public FlareGroupMemberRepository FlareGroupMemberRepository { get; set; }

        public FlareGroupService()
        {
            FlareGroupRepository = new FlareGroupRepository();

            FlareGroupMemberRepository = new FlareGroupMemberRepository();
        }

        public async Task<FlareGroup> CreateFlareGroup(FlareGroup flareGroup)
        {
            if (!ValidateFlareGroup(flareGroup)) throw new ArgumentException("Invalid Flare Group!");

            if (flareGroup.Id != Guid.Empty)
            {
                var existing = await UpdateFlareGroup(flareGroup);

                if (existing != null) return existing;

                flareGroup = await FlareGroupRepository.Create(flareGroup);
            }

            flareGroup.Id = Guid.NewGuid();

            

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

            flareGroup = await FlareGroupRepository.Update(flareGroup);

            return flareGroup;
        }

        public async Task<bool> DeleteFlareGroup(Guid id)
        {
            FlareGroup flareGroup = await GetFlareGroupById(id);

            if (flareGroup == null) throw new ArgumentException("The given flare group does not exist!");

            return await FlareGroupRepository.Delete(id);
        }

        private bool ValidateFlareGroup(FlareGroup flareGroup)
        {
            if (flareGroup.OwnerId == Guid.Empty) throw new ArgumentException("Flare group must have an admin!");

            if (flareGroup.OrganizationId == Guid.Empty) throw new ArgumentException("Flare group must have an organization!");

            if (string.IsNullOrWhiteSpace(flareGroup.Name)) throw new ArgumentException("Flare group must have a name!");

            return true;
        }
    }
}
