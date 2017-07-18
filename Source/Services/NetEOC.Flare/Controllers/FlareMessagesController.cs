using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetEOC.Flare.Models;
using NetEOC.Flare.Services;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Controllers
{
    [Route("flaremessages")]
    public class FlareMessagesController : BaseController
    {
        public FlareMessageService FlareMessageService { get; set; }

        public FlareMessagesController()
        {
            FlareMessageService = new FlareMessageService();
        }

        [Authorize, HttpGet]
        [SwaggerResponse(typeof(FlareMessage[]), Description = "Get all flare messages for the currently logged in user.")]
        public async Task<ActionResult> Get()
        {
            Guid userId = GetUserIdFromContext();

            return Ok(await FlareMessageService.GetFlareMessagesByRecipientId(userId));
        }

        [Authorize, HttpGet("{id}")]
        [SwaggerResponse(typeof(FlareMessage), Description = "Get the flare message with the given id")]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(await FlareMessageService.GetFlareMessageById(id));
        }

        [HttpPost("read/{token}")]
        [SwaggerResponse(typeof(FlareMessage), Description = "Set a flare message to read by supplying its token. This endpoint does not require a JWT or authorization.")]
        public async Task<ActionResult> Read(string token)
        {
            return Ok(await FlareMessageService.ReadFlareMessage(token));
        }

        [HttpPost("acknowledge/{token}")]
        [SwaggerResponse(typeof(FlareMessage), Description = "Set a flare message to acknowledged by supplying its token. This endpoint does not require a JWT or authorization.")]
        public async Task<ActionResult> Acknowledge(string token)
        {
            return Ok(await FlareMessageService.AcknowledgeFlareMessage(token));
        }
    }
}
