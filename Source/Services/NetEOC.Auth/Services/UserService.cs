using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetEOC.Auth.Models;
using NetEOC.Auth.Data;

namespace NetEOC.Auth.Services
{
    public class UserService
    {
        public UserRepository UserRepository { get; set; }

        public OrganizationRepository OrganizationRepository { get; set; }

        public OrganizationMemberRepository OrganizationMemberRepository { get; set; }

        public OrganizationAdminRepository OrganizationAdminRepository { get; set; }

        public UserService()
        {
            UserRepository = new UserRepository();

            OrganizationAdminRepository = new OrganizationAdminRepository();

            OrganizationMemberRepository = new OrganizationMemberRepository();
        }

        public async Task<User> Create(User user)
        {
            User existing;

            if(user.Id != Guid.Empty)
            {
                existing = await GetById(user.Id);

                if(existing == null)
                {
                    return await UserRepository.Create(user);
                }
            }

            existing = await GetByAuthId(user.AuthId);

            if (existing == null)
            {
                return await UserRepository.Create(user);
            }

            user.Id = existing.Id;

            return await Update(user);
        }

        public async Task<bool> Delete(Guid userId)
        {
            return await UserRepository.Delete(userId);
        }

        public async Task<User> GetByAuthId(string id)
        {
            return await UserRepository.GetByAuthId(id);
        }

        public async Task<User> GetById(Guid id)
        {
            return await UserRepository.Get(id);
        }

        public async Task<User> Update(User user)
        {
            if(user.Id == Guid.Empty || string.IsNullOrWhiteSpace(user.AuthId))
            {
                return null;
            }

            return await UserRepository.Update(user);
        }

        public async Task<bool> Validate(string authId, Guid id)
        {
            User user = await GetById(id);

            if (user == null) return false;

            return user.AuthId == authId;
        }

        public async Task<Guid[]> GetUserOrganizations(Guid userId)
        {
            Guid[] memberships = (await OrganizationMemberRepository.GetByUserId(userId)).Select(x => x.OrganizationId).ToArray();

            Guid[] administrations = (await OrganizationAdminRepository.GetByUserId(userId)).Select(x => x.OrganizationId).ToArray();

            Guid[] ownerships = (await OrganizationRepository.GetByOwnerId(userId)).Select(x => x.Id).ToArray();

            return memberships.Concat(administrations).Concat(ownerships).Distinct().ToArray();
        }
    }
}
