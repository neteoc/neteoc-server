using Microsoft.AspNetCore.Mvc;
using NetEOC.Flare.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Flare.Controllers
{
    [Route("groups")]
    public class FlareGroupController : BaseController
    {
        public FlareGroupService FlareGroupService { get; set; }

        public FlareGroupController()
        {
            FlareGroupService = new FlareGroupService();
        }

    }
}
