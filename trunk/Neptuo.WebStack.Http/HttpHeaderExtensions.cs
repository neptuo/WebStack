using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Extensions for type safe access to HTTP headers
    /// </summary>
    public static class HttpHeaderExtensions
    {
        /// <summary>
        /// Sets header 'Content-type'.
        /// </summary>
        public static IHttpResponse HeaderContentType(this IHttpResponse response, HttpMediaType mediaType)
        {
            return response.Header("Content-type", mediaType);
        }

        /// <summary>
        /// Gets request header 'Content-type'.
        /// </summary>
        public static HttpMediaType HeaderContentType(this IHttpRequest request)
        {
            return request.Header<HttpMediaType>("Content-type", null);
        }

        /// <summary>
        /// Gets request header 'Accept'.
        /// </summary>
        public static IEnumerable<HttpMediaType> HeaderAcceptList(this IHttpRequest request)
        {
            return request.Header<IEnumerable<HttpMediaType>>("Accept", Enumerable.Empty<HttpMediaType>());
        }

        /// <summary>
        /// Gets (first value from) request header 'Accept'.
        /// </summary>
        public static HttpMediaType HeaderAccept(this IHttpRequest request)
        {
            return request.HeaderAcceptList().FirstOrDefault();
        }
    }
}
