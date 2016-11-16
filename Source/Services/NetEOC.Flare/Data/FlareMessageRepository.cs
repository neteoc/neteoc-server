﻿using NetEOC.Shared.Aws.DynamoDb;
using NetEOC.Shared.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Data
{
    public class FlareMessageRepository : BaseDynamoRepository<Models.FlareMessage>
    {
        public override string TableName => ApplicationConfiguration.Configuration["dynamodb:tables:flareMessage"];

        public async Task<Models.FlareMessage[]> GetByFlareId(Guid flareId)
        {
            return await GetByIndex("FlareId-index", "FlareId", flareId.ToString());
        }

        public async Task<Models.FlareMessage[]> GetByRecipientId(Guid recipientId)
        {
            return await GetByIndex("RecipientId-index", "RecipientId", recipientId.ToString());
        }
    }
}
