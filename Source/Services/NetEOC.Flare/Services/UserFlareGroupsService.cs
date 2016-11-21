using NetEOC.Flare.Data;
using NetEOC.Flare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Services
{
    public class UserFlareGroupsService
    {
        public FlareGroupMemberRepository FlareGroupMemberRepository { get; set; }

        public FlareGroupRepository FlareGroupRepository { get; set; }

        public UserFlareGroupsService()
        {
            FlareGroupMemberRepository = new FlareGroupMemberRepository();

            FlareGroupRepository = new FlareGroupRepository();
        }

        public async Task<FlareGroupMember> AddUserToFlareGroup(Guid userId, Guid flareGroupId)
        {
            UserFlareGroups userFlareGroups = await UserFlareGroupsRepository.Get(userId);

            FlareGroup flareGroup = await FlareGroupRepository.Get(flareGroupId);

            if (flareGroup == null) throw new ArgumentException("The given flare group does not exist!");

            if (userFlareGroups == null)
            {
                userFlareGroups = new UserFlareGroups();

                userFlareGroups.Id = userId;

                userFlareGroups.FlareGroups = new List<Guid>();

                userFlareGroups = await UserFlareGroupsRepository.Create(userFlareGroups);
            }

            if (!userFlareGroups.FlareGroups.Contains(flareGroupId))
            {
                userFlareGroups.FlareGroups.Add(flareGroupId);

                userFlareGroups = await UserFlareGroupsRepository.Update(userFlareGroups);
            }

            if (!flareGroup.Members.Contains(userId))
            {
                flareGroup.Members.Add(userId);

                flareGroup = await FlareGroupRepository.Update(flareGroup);
            }

            return userFlareGroups;
        }

        public async Task<FlareGroupMember> RemoveUserFromFlareGroup(Guid userId, Guid flareGroupId)
        {
            UserFlareGroups userFlareGroups = await UserFlareGroupsRepository.Get(userId);

            FlareGroup flareGroup = await FlareGroupRepository.Get(flareGroupId);

            if (flareGroup == null) throw new ArgumentException("The given flare group does not exist!");

            if (userFlareGroups == null) return null;

            if (userFlareGroups.FlareGroups.Contains(flareGroupId))
            {
                userFlareGroups.FlareGroups.Remove(flareGroupId);

                userFlareGroups = await UserFlareGroupsRepository.Update(userFlareGroups);
            }

            if (flareGroup.Members.Contains(userId))
            {
                flareGroup.Members.Remove(userId);

                flareGroup = await FlareGroupRepository.Update(flareGroup);
            }

            return userFlareGroups;
        }
    }
}
