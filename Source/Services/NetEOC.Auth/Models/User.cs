using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetEOC.Shared.Aws.DynamoDb;

namespace NetEOC.Auth.Models
{
    public class User : BaseDynamoModel
    {
        public string AuthId { get; set; }

        public string Name { get; set; }

        public string Nickname { get; set; }

        public string Picture { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string SmsNumber { get; set; }

        public bool IsSiteAdmin { get; set; }
    }
}
