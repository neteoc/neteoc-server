using Amazon.DynamoDBv2.DocumentModel;
using NetEOC.Messaging.Models;
using NetEOC.Shared.Aws.DynamoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Messaging.Data
{
    public class FlareRepository : BaseDynamoRepository<Flare>
    {
        public override string TableName
        {
            get
            {
                return "EOCFlare";
            }
        }

        public async Task<Flare[]> GetBySenderId(Guid senderId)
        {
            return await GetByIndex("SenderId-index", "SenderId", senderId.ToString());
        }

        public async Task<Flare[]> GetByOrganizationId(Guid organizationId)
        {
            return await GetByIndex("OrganizationId-index", "OrganizationId", organizationId.ToString());
        }
    }
}
