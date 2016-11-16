using NetEOC.Shared.Aws.DynamoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Models
{
    public class FlareGroup : BaseDynamoModel
    {
        public Guid OwnerId { get; set; }

        public List<Guid> Members { get; set; }

        public Guid OrganizationId { get; set; }

        public string Name { get; set; }
    }
}
