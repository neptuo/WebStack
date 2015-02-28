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
        public static IReadOnlyKeyValueCollection Headers(this HttpRequest request)
        {
            Guard.NotNull(request, "request");

            IReadOnlyKeyValueCollection headers;
            if (!request.CustomValues().TryGet(RequestKey.Headers, out headers))
            {
                KeyValueCollection storage = new KeyValueCollection();
                foreach (KeyValuePair<string, string> header in request.RawMessage().Headers)
                    storage.Set(header.Key, header.Value);

                request.CustomValues().Set(RequestKey.Headers, headers = storage);
            }

            return headers;
        }

        /// <summary>
        /// HTTP request header.
        /// </summary>
        public static T Header<T>(this HttpRequest request, string headerName, T? defaltValue)
            where T : struct
        {
            Guard.NotNull(request, "request");
            Guard.NotNullOrEmpty(headerName, "headerName");
            return request.Headers().Get<T>(headerName, defaltValue);
        }

        /// <summary>
        /// HTTP request header.
        /// </summary>
        public static T Header<T>(this HttpRequest request, string headerName, T defaltValue)
            where T : class
        {
            Guard.NotNull(request, "request");
            Guard.NotNullOrEmpty(headerName, "headerName");
            return request.Headers().Get<T>(headerName, defaltValue);
        }
    }
}
