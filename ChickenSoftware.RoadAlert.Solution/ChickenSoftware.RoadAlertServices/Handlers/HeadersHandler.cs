using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http.Headers;

namespace ChickenSoftware.RoadAlertServices.Handlers
{
    public class HeadersHandler : DelegatingHandler
    {
        async protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Accept", "application/json;odata=verbose");

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            
            response.Content.Headers.Remove("DataServiceVersion");
            return response;
        }

    }
}