using System;
using System.Linq;
using System.Threading;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Repro.Function
{
    public class ProviderCallbackFunctions
    {
        // Because of bug here: https://github.com/Azure/azure-functions-host/issues/6932 we can't pass a code param to the azure function
        // So, we have to redirect to /oauth.html, which transforms the url parameters with underscores and redirects here.
        // Intended to be called as a browser navigation.
        [FunctionName("ProviderCallback")]
        public IActionResult ProviderCallback(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "login/callback/")] HttpRequest req,
            ILogger log, CancellationToken cancellation)
        {
            log.LogInformation("OAuth Callback starting");

            try
            {
                var code = req.Query["_code"].First();
                var state = req.Query["_state"].First();

                if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(state))
                {
                    log.LogWarning("Code or State value was null or empty.");
                    return new OkObjectResult("Code or State value was null or empty.");
                }

                // Do stuff
                return new OkObjectResult($"Got the following values - Code: {code} State: {state}. Function ready to do additional work.");

            }
            catch (Exception ex)
            {
                log.LogError(ex, "Unhandled error in OAuth Callback starting");
                return new RedirectResult("/");
            }
        }

    }
}
