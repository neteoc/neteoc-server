using NetEOC.Flare.Data;
using NetEOC.Flare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Services
{
    public class FlareGroupMemberService
    {
        public FlareGroupMemberRepository FlareGroupMemberRepository { get; set; }

        public FlareGroupRepository FlareGroupRepository { get; set; }

        public FlareGroupMemberService()
        {
            FlareGroupMemberRepository = new FlareGroupMemberRepository();

            FlareGroupRepository = new FlareGroupRepository();
        }

        public async Task<FlareGroupMember> AddUserToFlareGroup(Guid userId, Guid flareGroupId)
        {
            throw new NotImplementedException();
        }

        public async Task<FlareGroupMember> RemoveUserFromFlareGroup(Guid userId, Guid flareGroupId)
        {
            throw new NotImplementedException();
        }
    }
}
