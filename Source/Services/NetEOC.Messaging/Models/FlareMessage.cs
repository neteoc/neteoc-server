using NetEOC.Shared.Aws.DynamoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Messaging.Models
{
    public class FlareMessage : BaseDynamoModel
    {
        public Guid FlareId { get; set; }

        public Guid RecipientId { get; set; }

        public DateTime CreateDate { get; set; }

        public long Timestamp { get { return CreateDate.Ticks; } }

        public string MessageType { get; set; }
    }
}
