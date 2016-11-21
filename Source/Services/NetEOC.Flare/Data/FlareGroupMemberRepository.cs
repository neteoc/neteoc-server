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
        public override string TableName => ApplicationConfiguration.Configuration["dynamodb:tables:flareGroupMembers"];
    }
}
