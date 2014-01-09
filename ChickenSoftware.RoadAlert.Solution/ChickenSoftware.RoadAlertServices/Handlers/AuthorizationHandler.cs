using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ChickenSoftware.RoadAlertServices.Handlers
{
    public class AuthenticationHandler : DelegatingHandler
    {

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            if (IsRequestValid(request))
            {
                return base.SendAsync(request, cancellationToken);
            }
            else
            {
                using (HttpResponseMessage responseMessage = request.CreateResponse(HttpStatusCode.Unauthorized))
                {
                    responseMessage.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Basic"));
                    var taskCompletionSource = new TaskCompletionSource<HttpResponseMessage>();
                    taskCompletionSource.SetResult(responseMessage);
                    return taskCompletionSource.Task;
                }
            }
        }

        internal Boolean IsRequestValid(HttpRequestMessage request)
        {
            Boolean isValidRequest = false;
            var headers = request.Headers;
            if (headers.Authorization != null && headers.Authorization.Scheme == "Basic")
            {
                String authToken = "chickensoftware2014";
                //String encodedAuthToken = "Y2hpY2tlbnNvZnR3YXJlMjAxNA==";

                if (headers.Authorization.Parameter == authToken)
                {
                    isValidRequest = true;
                }

            }
            return isValidRequest;
        }
    }

}