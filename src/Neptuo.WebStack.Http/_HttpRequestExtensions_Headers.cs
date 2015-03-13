using Neptuo.Collections.Specialized;
using Neptuo.WebStack.Http.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Common extensions for <see cref="HttpRequest"/> for accessing HTTP headers.
    /// </summary>
    public static class _HttpRequestExtensions_Headers
    {
        public static HttpRequestHeaderCollection Headers(this HttpRequest httpRequest)
        {
            Ensure.NotNull(httpRequest, "request");

            HttpRequestHeaderCollection headers;
            if (!httpRequest.CustomValues().TryGet(RequestKey.Headers, out headers))
                httpRequest.CustomValues().Set(RequestKey.Headers, headers = new HttpRequestHeaderCollection(httpRequest.RawMessage()));

            return headers;
        }
    }
}
