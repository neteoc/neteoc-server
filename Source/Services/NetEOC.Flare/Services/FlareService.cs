using NetEOC.Flare.Data;
using NetEOC.Flare.Integrations.Messaging;
using NetEOC.Flare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetEOC.Flare.Services
{
    public class FlareService
    {
        public FlareRepository FlareRepository { get; set; }

        public FlareMessageRepository FlareMessageRepository { get; set; }

        public FlareGroupService FlareGroupService { get; set; }

        public MessagingService MessagingService { get; set; }

        public FlareService(string authorizationToken)
        {
            FlareRepository = new FlareRepository();

            FlareMessageRepository = new FlareMessageRepository();

            FlareGroupService = new FlareGroupService();

            MessagingService = new MessagingService(authorizationToken);
        }

        public async Task<Models.Flare> SendFlare(Models.Flare flare)
        {
            if (!ValidateFlare(flare)) throw new ArgumentException("Flare is invalid!");

            FlareGroup flareGroup = await FlareGroupService.GetFlareGroupById(flare.FlareGroupId);

            if (flareGroup == null) throw new ArgumentException("The given flare group does not exist!");

            flare.Recipients = flareGroup.Members.ToArray();

            throw new NotImplementedException();
        }

        private bool ValidateFlare(Models.Flare flare)
        {
            if (flare == null) throw new ArgumentException("Must provide a flare to send...");

            if (flare.FlareGroupId == Guid.Empty) throw new ArgumentException("To send a flare you specify a group!");

            if (flare.SenderId == Guid.Empty) throw new ArgumentException("To send a flare you specify a sender!");

            return true;
        }
    }
}
