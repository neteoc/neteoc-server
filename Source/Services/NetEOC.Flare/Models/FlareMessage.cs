using NetEOC.Shared.Aws.DynamoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Models
{
    public class FlareMessage : BaseDynamoModel
    {
        public Guid FlareId { get; set; }

        public Guid RecipientId { get; set; }

        public string MessageType { get; set; }

        public string Token { get; set; }

        public bool Acknowledged { get; set; }

        public bool Read { get; set; }
    }
}
