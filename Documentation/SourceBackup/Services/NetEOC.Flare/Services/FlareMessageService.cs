using NetEOC.Flare.Data;
using NetEOC.Flare.Models;
using NetEOC.Shared.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Services
{
    public class FlareMessageService
    {
        public FlareMessageRepository FlareMessageRepository { get; set; }

        public FlareMessageService()
        {
            FlareMessageRepository = new FlareMessageRepository();
        }

        public async Task<FlareMessage> GetFlareMessageById(Guid id)
        {
            return await FlareMessageRepository.Get(id);
        }

        public async Task<FlareMessage> GetFlareMessageByToken(string token)
        {
            return await FlareMessageRepository.GetByToken(token);
        }

        public async Task<FlareMessage[]> GetFlareMessagesByFlareId(Guid id)
        {
            return await FlareMessageRepository.GetByFlareId(id);
        }

        public async Task<FlareMessage[]> GetFlareMessagesByRecipientId(Guid id)
        {
            return await FlareMessageRepository.GetByRecipientId(id);
        }

        public async Task<FlareMessage> CreateFlareMessage(FlareMessage message, FlareMessageType messageType)
        {
            if (!ValidateFlareMessage(message)) throw new ArgumentException("Invalid Flare Message");

            if (message.Id == Guid.Empty)
            {
                message.Id = Guid.NewGuid();
            }

            if(string.IsNullOrWhiteSpace(message.Token))
            {
                CryptoProvider crypt = new CryptoProvider();

                message.Token = crypt.CreateUrlKey();
            }

            if (messageType == FlareMessageType.Email)
            {
                message.MessageType = "email";
            }
            else if(messageType == FlareMessageType.Sms)
            {
                message.MessageType = "sms";
            }
            else if(messageType == FlareMessageType.PhoneCall)
            {
                message.MessageType = "phone";
            }

            message = await FlareMessageRepository.Create(message);

            return message;
        }

        public async Task<FlareMessage> ReadFlareMessage(Guid id)
        {
            FlareMessage message = await GetFlareMessageById(id);

            if (message == null) return null;

            message.Read = true;

            return await FlareMessageRepository.Update(message);
        }

        public async Task<FlareMessage> ReadFlareMessage(string token)
        {
            FlareMessage message = await GetFlareMessageByToken(token);

            if (message == null) return null;

            message.Read = true;

            return await FlareMessageRepository.Update(message);
        }

        public async Task<FlareMessage> AcknowledgeFlareMessage(Guid id)
        {
            FlareMessage message = await GetFlareMessageById(id);

            if (message == null) return null;

            message.Read = true;

            message.Acknowledged = true;

            return await FlareMessageRepository.Update(message);
        }

        public async Task<FlareMessage> AcknowledgeFlareMessage(string token)
        {
            FlareMessage message = await GetFlareMessageByToken(token);

            if (message == null) return null;

            message.Read = true;

            message.Acknowledged = true;

            return await FlareMessageRepository.Update(message);
        }

        private bool ValidateFlareMessage(FlareMessage message)
        {
            if (message.RecipientId == Guid.Empty) throw new ArgumentException("Flare Message must have a recipient id!");

            if (message.FlareId == Guid.Empty) throw new ArgumentException("Flare Message must have a flare id!");

            return true;
        }
    }
}
