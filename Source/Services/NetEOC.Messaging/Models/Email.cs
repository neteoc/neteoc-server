using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Messaging.Models
{
    public class Email
    {
        public string Recipient { get; set; }

        public string Body { get; set; }

        public bool IsHtml { get; set; }
    }
}
