using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Integrations.Messaging
{
    public class Email
    {
        public string Recipient { get; set; }

        public string Body { get; set; }

        public bool IsHtml { get; set; }
    }
}
