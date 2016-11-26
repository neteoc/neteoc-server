using NetEOC.Shared.Aws.DynamoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Auth.Models
{
    public class Organization : BaseDynamoModel
    {
        public Organization()
        {
            Data = new Dictionary<string, string>(); //verify that data is always at least an empy dictionary
        }

        public Guid OwnerId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Dictionary<string, string> Data { get; set; }
    }
}
