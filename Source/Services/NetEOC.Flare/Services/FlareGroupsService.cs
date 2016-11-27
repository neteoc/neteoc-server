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

                return flareGroup;
            }

            flareGroup.Id = Guid.NewGuid();

            flareGroup = await FlareGroupRepository.Create(flareGroup);

            return flareGroup;
        }

        public async Task<FlareGroup[]> GetFlareGroupsByUserId(Guid userId)
        {
            FlareGroupMember[] memberships = await FlareGroupMemberRepository.GetByUserId(userId);

            FlareGroup[] membershipGroups = await Task.WhenAll(memberships.Select(x => x.FlareGroupId).Select(FlareGroupRepository.Get));

            FlareGroup[] ownershipGroups = await FlareGroupRepository.GetByOwnerId(userId);

            return membershipGroups.Concat(ownershipGroups).GroupBy(x => x.Id).Select(x => x.First()).ToArray();
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

            if (existing == null) return null;

            //merge values

            existing.Description = flareGroup.Description;

            existing.Name = flareGroup.Name;

            if (flareGroup.Data != null)
            {
                foreach (var kv in flareGroup.Data)
                {
                    if (existing.Data.ContainsKey(kv.Key))
                    {
                        existing.Data[kv.Key] = kv.Value;
                    }
                    else
                    {
                        existing.Data.Add(kv.Key, kv.Value);
                    }
                }
            }

            flareGroup = await FlareGroupRepository.Update(flareGroup);

            return flareGroup;
        }

        public async Task<bool> DeleteFlareGroup(Guid id)
        {
            FlareGroup flareGroup = await GetFlareGroupById(id);

            if (flareGroup == null) return false;

            var memberships = await FlareGroupMemberRepository.GetByFlareGroupId(id);

            await Task.WhenAll(memberships.Select(x => x.Id).Select(FlareGroupMemberRepository.Delete));

            return await FlareGroupRepository.Delete(id);
        }

        private bool ValidateFlareGroup(FlareGroup flareGroup)
        {
            if (flareGroup.OwnerId == Guid.Empty) throw new ArgumentException("Flare group must have an admin!");

            if (flareGroup.OrganizationId == Guid.Empty) throw new ArgumentException("Flare group must have an organization!");

            if (string.IsNullOrWhiteSpace(flareGroup.Name)) throw new ArgumentException("Flare group must have a name!");

            return true;
        }

        public async Task<FlareGroupMember> AddMemberToFlareGroup(Guid flareGroupId, Guid memberId)
        {
            FlareGroupMember[] memberships = await FlareGroupMemberRepository.GetByFlareGroupId(flareGroupId);

            FlareGroupMember membership = memberships.FirstOrDefault(x => x.UserId == memberId);

            if (membership != null) return membership;

            membership = new FlareGroupMember { FlareGroupId = flareGroupId, UserId = memberId, Id = Guid.NewGuid() };

            return await FlareGroupMemberRepository.Create(membership);
        }

        public async Task<bool> RemoveMemberFromFlareGroup(Guid flareGroupId, Guid memberId)
        {
            FlareGroupMember[] memberships = await FlareGroupMemberRepository.GetByFlareGroupId(flareGroupId);

            FlareGroupMember membership = memberships.FirstOrDefault(x => x.UserId == memberId);

            if (membership == null) return true;

            return await FlareGroupMemberRepository.Delete(membership.Id);
        }

        public async Task<Guid[]> GetFlareGroupMembers(Guid flareGroupId)
        {
            FlareGroupMember[] memberships = await FlareGroupMemberRepository.GetByFlareGroupId(flareGroupId);

            return memberships.Select(x => x.UserId).Distinct().ToArray();
        }
    }
}
