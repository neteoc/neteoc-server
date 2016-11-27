using NetEOC.Flare.Models;
using NetEOC.Shared.Aws.DynamoDb;
using NetEOC.Shared.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Data
{
    public class FlareGroupMemberRepository : BaseDynamoRepository<FlareGroupMember>
    {
        public override string TableName => ApplicationConfiguration.Configuration["dynamodb:tables:flareGroupMember"];

        public async Task<FlareGroupMember[]> GetByUserId(Guid userId)
        {
            return await GetByIndex("UserId-index", "UserId", userId.ToString());
        }

        public async Task<FlareGroupMember[]> GetByFlareGroupId(Guid flareGroupId)
        {
            return await GetByIndex("FlareGroupId-index", "FlareGroupId", flareGroupId.ToString());
        }
    }
}
