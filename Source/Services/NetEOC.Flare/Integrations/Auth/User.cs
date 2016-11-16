using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Integrations.Auth
{
    public class User
    {
        public Guid Id { get; set; }

        public string AuthId { get; set; }

        public string Name { get; set; }

        public string Nickname { get; set; }

        public string Picture { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
