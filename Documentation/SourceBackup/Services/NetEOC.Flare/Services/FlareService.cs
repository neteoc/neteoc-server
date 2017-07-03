using NetEOC.Flare.Data;
using NetEOC.Flare.Integrations.Messaging;
using NetEOC.Flare.Models;
using NetEOC.Shared.Util;
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

        public FlareMessageService FlareMessageService { get; set; }

        public FlareGroupService FlareGroupService { get; set; }

        public FlareService()
        {
            FlareRepository = new FlareRepository();

            FlareMessageService = new FlareMessageService();

            FlareGroupService = new FlareGroupService();
        }

        public async Task<Models.Flare> SendFlare(Models.Flare flare)
        {
            if (!ValidateFlare(flare)) throw new ArgumentException("Flare is invalid!");

            flare.Id = Guid.NewGuid();

            flare = await FlareRepository.Create(flare);

            CryptoProvider crypt = new CryptoProvider();

            if(flare.UseEmail)
            {
                FlareMessage[] emails = flare.Recipients.Select(x => { return new FlareMessage { RecipientId = x, FlareId = flare.Id }; }).ToArray();

                emails = await Task.WhenAll(emails.Select(x=>FlareMessageService.CreateFlareMessage(x, FlareMessageType.Email)));
            }

            if (flare.UseSms)
            {
                FlareMessage[] texts = flare.Recipients.Select(x => { return new FlareMessage { RecipientId = x, FlareId = flare.Id }; }).ToArray();

                texts = await Task.WhenAll(texts.Select(x => FlareMessageService.CreateFlareMessage(x, FlareMessageType.Sms)));
            }

            if (flare.UsePhone)
            {
                FlareMessage[] calls = flare.Recipients.Select(x => { return new FlareMessage { RecipientId = x, FlareId = flare.Id }; }).ToArray();

                calls = await Task.WhenAll(calls.Select(x => FlareMessageService.CreateFlareMessage(x, FlareMessageType.PhoneCall)));
            }

            return flare;
        }

        public async Task<Models.Flare> GetFlareById(Guid id)
        {
            return await FlareRepository.Get(id);
        }

        public async Task<Models.Flare[]> GetFlaresBySenderId(Guid id)
        {
            return await FlareRepository.GetBySenderId(id);
        }

        public async Task<Models.Flare[]> GetFlaresByOrganizationId(Guid id)
        {
            return await FlareRepository.GetByOrganizationId(id);
        }

        private bool ValidateFlare(Models.Flare flare)
        {
            if (flare == null) throw new ArgumentException("Must provide a flare to send...");

            if (flare.SenderId == Guid.Empty) throw new ArgumentException("To send a flare you specify a sender!");

            if (flare.OrganizationId == Guid.Empty) throw new ArgumentException("To send a flare you must specify an organization!");

            if (flare.Recipients == null || flare.Recipients.Length < 1) throw new ArgumentException("A flare must have recipients!");

            return true;
        }
    }
}
