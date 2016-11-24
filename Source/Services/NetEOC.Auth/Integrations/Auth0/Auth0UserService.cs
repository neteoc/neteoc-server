using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceStack;
using Auth0.ManagementApi;
using NetEOC.Shared.Configuration;
using Auth0.ManagementApi.Models;
using Auth0.Core;
using System.Dynamic;

namespace NetEOC.Auth.Integrations.Auth0
{
    public class Auth0UserService
    {
        public async Task<bool> SetUserLocalId(string authId, string localId)
        {
            string apiToken = ApplicationConfiguration.Configuration["auth0:managementApiToken"];

            string apiEndpoint = ApplicationConfiguration.Configuration["auth0:domain"];

            ManagementApiClient _client = new ManagementApiClient(apiToken, apiEndpoint);

            UserUpdateRequest request = new UserUpdateRequest();

            request.AppMetadata = new ExpandoObject();

            request.AppMetadata.neteoc_id = localId;

            User user = await _client.Users.UpdateAsync(authId, request);

            return true;
        }
    }
}
