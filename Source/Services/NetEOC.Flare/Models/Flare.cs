﻿using NetEOC.Shared.Aws.DynamoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Models
{
    public class Flare : BaseDynamoModel
    {
        public Guid SenderId { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid[] Recipients { get; set; }

        public bool UseEmail { get; set; }

        public bool UseSms { get; set; }

        public bool UsePhone { get; set; }

        public string ShortTitle { get; set; }

        public string LongTitle { get; set; }

        public string ShortContent { get; set; }

        public string LongContent { get; set; }

        public string Description { get; set; }

        public string[] Attachments { get; set; }
    }
}
