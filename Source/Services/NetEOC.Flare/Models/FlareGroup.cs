﻿using NetEOC.Shared.Aws.DynamoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Models
{
    public class FlareGroup : BaseDynamoModel
    {
        public Guid OrganizationId { get; set; }

        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public long CreateDateTicks { get { return CreateDate.Ticks; } }
    }
}
