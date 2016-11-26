using NetEOC.Shared.Aws.DynamoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Models
{
    public class FlareGroup : BaseDynamoModel
    {
        public FlareGroup()
        {
            Data = new Dictionary<string, string>();
        }

        public Guid OwnerId { get; set; }

        public Guid OrganizationId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Dictionary<string, string> Data { get; set; }
    }
}
