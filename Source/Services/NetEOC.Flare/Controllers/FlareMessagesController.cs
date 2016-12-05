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

        [Authorize, HttpGet("{id}")]
        [SwaggerResponse(typeof(FlareMessage), Description = "Get the flare message with the given id")]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(await FlareMessageService.GetFlareMessageById(id));
        }
    }
}
