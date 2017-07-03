using NetEOC.Shared.Aws.DynamoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Auth.Models
{
    public class OrganizationAdmin : BaseDynamoModel
    {
        public Guid UserId { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
