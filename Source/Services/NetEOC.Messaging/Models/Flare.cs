using NetEOC.Shared.Aws.DynamoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Messaging.Models
{
    public class Flare : BaseDynamoModel
    {
        public Guid SenderId { get; set; }

        public Guid OrganizationId { get; set; }

        public bool UseEmail { get; set; }

        public bool UseSms { get; set; }

        public bool UsePhone { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public DateTime SendDate { get; set; }
    }
}
