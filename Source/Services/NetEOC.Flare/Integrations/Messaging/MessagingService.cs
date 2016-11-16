using NetEOC.Shared.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Integrations.Messaging
{
    public class MessagingService
    {
        public string MessagingEndpoint { get; set; }

        public string AuthorizationToken { get; set; }

        public MessagingService(string authorizationToken)
        {
            AuthorizationToken = authorizationToken;

            MessagingEndpoint = ApplicationConfiguration.Configuration["services:messaging"];
        }

        public async Task<bool> SendEmail(Email email)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SendSms(Sms sms)
        {
            throw new NotImplementedException();
        }
    }
}
