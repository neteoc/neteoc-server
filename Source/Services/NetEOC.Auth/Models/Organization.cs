using NetEOC.Shared.Aws.DynamoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Auth.Models
{
    public class Organization : BaseDynamoModel
    {
        public Guid OwnerId { get; set; }

        public List<Guid> Admins { get; set; }

        public List<Guid> Members { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Data { get; set; }
    }
}
