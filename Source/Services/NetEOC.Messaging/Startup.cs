using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors.Infrastructure;
using NetEOC.Shared.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace NetEOC.Messaging
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            //enable cors
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", corsBuilder.Build());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //setup logging
            loggerFactory.AddConsole(ApplicationConfiguration.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //setup auth
            var keyAsBase64 = ApplicationConfiguration.Configuration["auth0:clientSecret"].Replace('_', '/').Replace('-', '+');
            var keyAsBytes = Convert.FromBase64String(keyAsBase64);
            var options = new JwtBearerOptions
            {
                TokenValidationParameters =
                {
                    ValidIssuer = $"https://{ApplicationConfiguration.Configuration["auth0:domain"]}/",
                    ValidAudience = ApplicationConfiguration.Configuration["auth0:clientId"],
                    IssuerSigningKey = new SymmetricSecurityKey(keyAsBytes)
                }
            };
            app.UseJwtBearerAuthentication(options);

            //setup mvc
            app.UseMvc();

            //setup cors
            app.UseCors("AllowAll");
        }
    }
}
