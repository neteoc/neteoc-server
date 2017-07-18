using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Auth.Controllers
{
    public abstract class BaseController : Controller
    {
        [SwaggerIgnore]
        public string GetAuthIdFromContext()
        {
            return User.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
        }

        [SwaggerIgnore]
        public Guid GetUserIdFromContext()
        {
            var claim = User.Claims.FirstOrDefault(x => x.Type == "neteoc_id");

            if (claim == null) return Guid.Empty;

            return Guid.Parse(claim.Value);
        }
    }
}
