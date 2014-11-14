using Neptuo.Collections.Specialized;
using Neptuo.WebStack.Http.Keys;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Describes Http response.
    /// </summary>
    public interface IHttpResponse
    {
        /// <summary>
        /// Collection of supported values.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IKeyValueCollection Values { get; }
    }

    public static class HttpResponseExtensions
    {
        /// <summary>
        /// Http response status.
        /// </summary>
        public static HttpStatus Status(this IHttpResponse response)
        {
            Guard.NotNull(response, "response");

            HttpStatus status;
            if (!response.Values.TryGet(ResponseKey.Status, out status))
                status = response.Status(HttpStatus.Ok);

            return status;
        }

        /// <summary>
        /// Http response status.
        /// </summary>
        public static HttpStatus Status(this IHttpResponse response, HttpStatus status)
        {
            Guard.NotNull(response, "response");
            Guard.NotNull(status, "status");
            response.Values.Set(ResponseKey.Status, status);
            return status;
        }

        /// <summary>
        /// Http response headers.
        /// </summary>
        public static IKeyValueCollection Headers(this IHttpResponse response)
        {
            Guard.NotNull(response, "response");
            return response.Values.Get<IKeyValueCollection>(ResponseKey.Headers);
        }

        /// <summary>
        /// Sets header name <paramref name="headerName"/> to value <paramref name="headerValue"/>.
        /// </summary>
        /// <param name="headerName">Header name.</param>
        /// <param name="headerValue">Header value.</param>
        public static IHttpResponse Headers(this IHttpResponse response, string headerName, string headerValue)
        {
            Guard.NotNull(response, "response");
            Guard.NotNullOrEmpty(headerName, "headerName");
            response.Headers().Set(headerName, headerValue);
            return response;
        }

        /// <summary>
        /// Response stream.
        /// </summary>
        public static Stream OutputStream(this IHttpResponse response)
        {
            Guard.NotNull(response, "response");
            return response.Values.Get<Stream>(ResponseKey.OutputStream);
        }

        /// <summary>
        /// Response text writer.
        /// </summary>
        public static TextWriter OutputWriter(this IHttpResponse response)
        {
            Guard.NotNull(response, "response");

            TextWriter writer;
            if (!response.Values.TryGet<TextWriter>(ResponseKey.OutputWriter, out writer))
            {
                writer = new StreamWriter(response.OutputStream());
                response.Values.Set(ResponseKey.OutputWriter, writer);
            }

            return writer;
        }
    }
}
