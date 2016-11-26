﻿using NetEOC.Shared.Aws.DynamoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Models
{
    public class Flare : BaseDynamoModel
    {
        public Flare()
        {
            Data = new Dictionary<string, string>();
        }

        public Guid SenderId { get; set; }

        public Guid FlareGroupId { get; set; }

        public Guid[] Recipients { get; set; }

        public bool UseEmail { get; set; }

        public bool UseSms { get; set; }

        public bool UsePhone { get; set; }

        public string ShortTitle { get; set; }

        public string LongTitle { get; set; }

        public string ShortContent { get; set; }

        public string LongContent { get; set; }

        public string Description { get; set; }

        public string AttachmentUrl { get; set; }

        public string Address { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public Dictionary<string, string> Data { get; set; }
    }
}
