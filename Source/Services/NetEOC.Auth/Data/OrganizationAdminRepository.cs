using NetEOC.Auth.Models;
using NetEOC.Shared.Aws.DynamoDb;
using NetEOC.Shared.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Auth.Data
{
    public class OrganizationAdminRepository : BaseDynamoRepository<OrganizationAdmin>
    {
        public override string TableName => ApplicationConfiguration.Configuration["dynamodb:tables:organizationAdmin"];

        public async Task<OrganizationAdmin[]> GetByUserId(Guid userId)
        {
            return await GetByIndex("UserId-index", "UserId", userId.ToString());
        }

        public async Task<OrganizationAdmin[]> GetByOrganizationId(Guid organizationId)
        {
            return await GetByIndex("OrganizationId-index", "OrganizationId", organizationId.ToString());
        }
    }
}
