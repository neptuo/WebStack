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
    /// Common extensions for <see cref="HttpResponse"/> for accessing HTTP headers.
    /// </summary>
    public static class _HttpResponseExtensions_Headers
    {
        public static HttpResponseHeaderCollection Headers(this HttpResponse httpResponse)
        {
            Ensure.NotNull(httpResponse, "httpResponse");

            HttpResponseHeaderCollection headers;
            if (!httpResponse.CustomValues().TryGet(ResponseKey.Headers, out headers))
                httpResponse.CustomValues().Set(ResponseKey.Headers, headers = new HttpResponseHeaderCollection(httpResponse.RawMessage()));

            return headers;
        }
    }
}
