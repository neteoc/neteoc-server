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

        public Guid[] Recipients { get; set; }

        public DateTime CreateDate { get; set; }

        public long Timestamp { get { return CreateDate.Ticks; } }

        public bool UseEmail { get; set; }

        public bool UseSms { get; set; }

        public bool UsePhone { get; set; }

        public string ShortTitle { get; set; }

        public string LongTitle { get; set; }

        public string Content { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string AttachmentUrl { get; set; }

        public string Address { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string Data { get; set; }
    }
}
