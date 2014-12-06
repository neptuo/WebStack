using Neptuo.Collections.Specialized;
using Neptuo.WebStack.Http.Keys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Common extensions for <see cref="IHttpResponse"/>.
    /// </summary>
    public static class _HttpResponseExtensions
    {
        /// <summary>
        /// Sets header name <paramref name="headerName"/> to value <paramref name="headerValue"/>.
        /// </summary>
        /// <param name="headerName">Header name.</param>
        /// <param name="headerValue">Header value.</param>
        public static IHttpResponse Header(this IHttpResponse response, string headerName, object headerValue)
        {
            Guard.NotNull(response, "response");
            Guard.NotNullOrEmpty(headerName, "headerName");
            response.Headers().Set(headerName, headerValue);
            return response;
        }

        /// <summary>
        /// Http request headers.
        /// </summary>
        public static T HeaderValue<T>(this IHttpResponse response, string headerName, T defaultValue)
        {
            Guard.NotNull(response, "request");
            Guard.NotNullOrEmpty(headerName, "headerName");

            T value;
            if (response.Headers().TryGet(headerName, out value))
                return value;

            return defaultValue;
        }

        /// <summary>
        /// Response text writer.
        /// </summary>
        public static TextWriter OutputWriter(this IHttpResponse response)
        {
            Guard.NotNull(response, "response");

            TextWriter writer;
            if (!response.CustomValues().TryGet<TextWriter>(ResponseKey.OutputWriter, out writer))
            {
                writer = new StreamWriter(response.OutputStream()) { AutoFlush = true };
                response.CustomValues().Set(ResponseKey.OutputWriter, writer);
                response.OnDisposing += writer.Flush;
            }

            return writer;
        }
    }
}
