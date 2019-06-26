using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeSample.Http
{
    public class JsonHeaderHandler:DelegatingHandler
    {
        public JsonHeaderHandler(HttpMessageHandler handler)
        {
            InnerHandler = handler;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json")
            {
                CharSet = "utf-8"
            };
            return base.SendAsync(request, cancellationToken);
        }
    }
}
