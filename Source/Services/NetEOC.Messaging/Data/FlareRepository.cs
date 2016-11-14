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
    }
}
