using NetEOC.Shared.Aws.DynamoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Models
{
    public class UserFlareGroups : BaseDynamoModel
    {
        public List<Guid> FlareGroups { get; set; }
    }
}
