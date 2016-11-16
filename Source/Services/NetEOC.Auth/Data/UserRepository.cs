using NetEOC.Auth.Models;
using NetEOC.Shared.Aws.DynamoDb;
using NetEOC.Shared.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Auth.Data
{
    public class UserRepository : BaseDynamoRepository<User>
    {
        public override string TableName => ApplicationConfiguration.Configuration["dynamodb:tables:user"];

        public async Task<User> GetByAuthId(string authId)
        {
            return (await GetByIndex("AuthId-index", "AuthId", authId)).FirstOrDefault();
        }
    }
}
