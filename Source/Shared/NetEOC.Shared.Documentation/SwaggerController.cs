using Microsoft.AspNetCore.Mvc;
using NSwag.CodeGeneration.SwaggerGenerators.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetEOC.Auth.Controllers
{
    public class SwaggerController : Controller
    {
        private static readonly Lazy<byte[]> _swagger = new Lazy<byte[]>(() =>
        {
            var types = Assembly.GetEntryAssembly().GetTypes();
            var controllers = types.Where(x => !x.GetTypeInfo().IsAbstract && typeof(Controller).IsAssignableFrom(x) && x != typeof(Controller)).ToArray();
            var settings = new WebApiToSwaggerGeneratorSettings
            {
                IsAspNetCore = true,
                DefaultPropertyNameHandling = NJsonSchema.PropertyNameHandling.CamelCase
            };
            var generator = new WebApiToSwaggerGenerator(settings);
            var document = generator.GenerateForControllers(controllers);
            return Encoding.UTF8.GetBytes(document.ToJson());
        });

        [HttpGet, Route("/swagger")]
        public ActionResult Swagger()
        {
            return new FileContentResult(_swagger.Value, "application/json");
        }
    }
}
