using NetEOC.Auth.Models;
using NetEOC.Shared.Aws.DynamoDb;
using NetEOC.Shared.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Auth.Data
{
    public class OrganizationRepository : BaseDynamoRepository<Organization>
    {
        public override string TableName => ApplicationConfiguration.Configuration["dynamodb:tables:organization"];

        public async Task<Organization[]> GetByOwnerId(Guid ownerId)
        {
            return await GetByIndex("OwnerId-index", "OwnerId", ownerId.ToString());
        }
    }
}