using NetEOC.Auth.Data;
using NetEOC.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Auth.Services
{
    public class OrganizationService
    {
        public OrganizationRepository OrganizationRepository { get; set; } 

        public OrganizationMemberRepository OrganizationMemberRepository { get; set; }

        public OrganizationAdminRepository OrganizationAdminRepository { get; set; }

        public OrganizationService()
        {
            OrganizationRepository = new OrganizationRepository();

            OrganizationAdminRepository = new OrganizationAdminRepository();

            OrganizationMemberRepository = new OrganizationMemberRepository();
        }

        public async Task<Organization> Create(Organization organization)
        {
            if (!ValidateOrganization(organization)) throw new ArgumentException("Invalid Organization!");

            if(organization.Id != Guid.Empty)
            {
                Organization existing = await Update(organization);

                if (existing != null) return existing;
            }

            return await OrganizationRepository.Create(organization);
        }

        public async Task<Organization> GetById(Guid id)
        {
            return await OrganizationRepository.Get(id);
        }

        public async Task<Organization[]> GetByOwnerId(Guid ownerId)
        {
            return await OrganizationRepository.GetByOwnerId(ownerId);
        }

        public async Task<Organization> Update(Organization organization)
        {
            if (!ValidateOrganization(organization)) throw new ArgumentException("Invalid Organization!");

            if (organization.Id == Guid.Empty) throw new ArgumentException("Organization has no id!");

            //merge organization

            Organization existing = await GetById(organization.Id);

            if (existing == null) return null;

            if (!string.IsNullOrWhiteSpace(organization.Name)) existing.Name = organization.Name;

            if (!string.IsNullOrWhiteSpace(organization.Description)) existing.Description = organization.Description;

            if (organization.Data != null)
            {
                foreach (var kv in organization.Data)
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

            return await OrganizationRepository.Update(existing);
        }

        public async Task<bool> Delete(Guid organizationId)
        {
            return await OrganizationRepository.Delete(organizationId);
        }

        public async Task<Guid[]> GetOrganizationMembers(Guid organizationId)
        {
            return (await OrganizationMemberRepository.GetByOrganizationId(organizationId)).Select(x => x.UserId).ToArray();
        }

        public async Task<Guid[]> GetOrganizationAdmins(Guid organizationId)
        {
            return (await OrganizationAdminRepository.GetByOrganizationId(organizationId)).Select(x => x.UserId).ToArray();
        }

        public async Task<OrganizationMember> AddMemberToOrganization(Guid organizationId, Guid userId)
        {
            OrganizationMember[] memberships = await OrganizationMemberRepository.GetByUserId(userId);

            OrganizationMember membership = memberships.FirstOrDefault(x => x.OrganizationId == organizationId);

            if (membership == null)
            {
                membership = new OrganizationMember { OrganizationId = organizationId, UserId = userId };

                membership = await OrganizationMemberRepository.Create(membership);
            }

            return membership;
        }

        public async Task<bool> RemoveMemberFromOrganization(Guid organizationId, Guid userId)
        {
            OrganizationMember[] memberships = await OrganizationMemberRepository.GetByUserId(userId);

            OrganizationMember membership = memberships.FirstOrDefault(x => x.OrganizationId == organizationId);

            bool success = false;

            if (membership != null)
            {
                success = await OrganizationMemberRepository.Delete(membership.Id);
            }

            return success;
        }

        public async Task<OrganizationAdmin> AddAdminToOrganization(Guid organizationId, Guid userId)
        {
            OrganizationAdmin[] memberships = await OrganizationAdminRepository.GetByUserId(userId);

            OrganizationAdmin membership = memberships.FirstOrDefault(x => x.OrganizationId == organizationId);

            if (membership == null)
            {
                membership = new OrganizationAdmin { OrganizationId = organizationId, UserId = userId };

                membership = await OrganizationAdminRepository.Create(membership);
            }

            return membership;
        }

        public async Task<bool> RemoveAdminFromOrganization(Guid organizationId, Guid userId)
        {
            OrganizationAdmin[] memberships = await OrganizationAdminRepository.GetByUserId(userId);

            OrganizationAdmin membership = memberships.FirstOrDefault(x => x.OrganizationId == organizationId);

            bool success = false;

            if (membership != null)
            {
                success = await OrganizationAdminRepository.Delete(membership.Id);
            }

            return success;
        }


        private bool ValidateOrganization(Organization organization)
        {
            if (organization.OwnerId == Guid.Empty) throw new ArgumentException("Organization must have an owner!");

            if (string.IsNullOrWhiteSpace(organization.Name)) throw new ArgumentException("Organization must have a name!");

            return true;
        }
    }
}
