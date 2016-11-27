using NetEOC.Flare.Models;
using NetEOC.Shared.Aws.DynamoDb;
using NetEOC.Shared.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Data
{
    public class FlareGroupRepository : BaseDynamoRepository<FlareGroup>
    {
        public override string TableName => ApplicationConfiguration.Configuration["dynamodb:tables:flareGroup"];

        public async Task<FlareGroup[]> GetByOrganizationId(Guid id)
        {
            return await GetByIndex("OrganizationId-index", "OrganizationId", id.ToString());
        }

        public async Task<FlareGroup[]> GetByOwnerId(Guid id)
        {
            return await GetByIndex("OwnerId-index", "OwnerId", id.ToString());
        }
    }
}
