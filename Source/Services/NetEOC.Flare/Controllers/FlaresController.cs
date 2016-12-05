using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetEOC.Flare.Models;
using NetEOC.Flare.Data;
using Microsoft.AspNetCore.Authorization;
using NSwag.Annotations;
using NetEOC.Flare.Services;

namespace NetEOC.Flare.Controllers
{
    [Route("flares")]
    public class FlaresController : BaseController
    {
        public FlareService FlareService { get; set; }

        public FlareMessageService FlareMessageService { get; set; }

        public FlaresController()
        {
            FlareService = new FlareService();

            FlareMessageService = new FlareMessageService();
        }

        [Authorize, HttpGet]
        [SwaggerResponse(typeof(Models.Flare[]), Description = "Get all flares that the currently logged in user has created.")]
        public async Task<ActionResult> Get()
        {
            return Ok(await FlareService.GetFlaresBySenderId(GetUserIdFromContext()));
        }

        [Authorize, HttpGet("{id}")]
        [SwaggerResponse(typeof(Models.Flare), Description = "Get the flare with the given id.")]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(await FlareService.GetFlareById(id));
        }

        [Authorize, HttpPost]
        [SwaggerResponse(typeof(Models.Flare), Description = "Create a new flare and send it.")]
        public async Task<ActionResult> Post([FromBody] Models.Flare flare)
        {
            return Ok(await FlareService.SendFlare(flare));
        }

        [Authorize, HttpGet("{id}/flaremessages")]
        [SwaggerResponse(typeof(FlareMessage[]), Description = "Get all flare messages for the flare with the given id.")]
        public async Task<ActionResult> GetFlareMessages(Guid id)
        {
            return Ok(await FlareMessageService.GetFlareMessagesByFlareId(id));
        }
    }
}
