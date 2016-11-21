using NetEOC.Shared.Aws.DynamoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Models
{
    public class FlareGroupMember : BaseDynamoModel
    {
        public Guid UserId { get; set; }

        public Guid FlareGroupId { get; set; }
    }
}
