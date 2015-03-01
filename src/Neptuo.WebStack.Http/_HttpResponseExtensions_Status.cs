using Neptuo.WebStack.Http.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Common extensions for <see cref="HttpResponse"/> for providing status code.
    /// </summary>
    public static class _HttpResponseExtensions_Status
    {
        public static HttpResponse Status(this HttpResponse httpResponse, HttpStatus status)
        {
            Guard.NotNull(httpResponse, "httpResponse");
            Guard.NotNull(status, "status");

            httpResponse.CustomValues().Set(ResponseKey.Status, status);
            httpResponse.RawMessage().StatusCode = status.Code;
            httpResponse.RawMessage().StatusText = status.Text;
            return httpResponse;
        }
    }
}
