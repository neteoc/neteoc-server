using Amazon.DynamoDBv2.DocumentModel;
using NetEOC.Shared.Aws.DynamoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Data
{
    public class FlareRepository : BaseDynamoRepository<Models.Flare>
    {
        public override string TableName
        {
            get
            {
                return "EOCFlare";
            }
        }

        public async Task<Models.Flare[]> GetBySenderId(Guid senderId)
        {
            return await GetByIndex("SenderId-index", "SenderId", senderId.ToString());
        }

        public async Task<Models.Flare[]> GetByOrganizationId(Guid organizationId)
        {
            return await GetByIndex("OrganizationId-index", "OrganizationId", organizationId.ToString());
        }
    }
}
