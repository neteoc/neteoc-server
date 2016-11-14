using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Shared.Aws.DynamoDb
{
    public class BaseDynamoModel
    {
        public Guid Id { get; set; }

        public string Type => GetType().Name;
    }
}
