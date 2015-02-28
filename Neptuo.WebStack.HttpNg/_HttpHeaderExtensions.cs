using Neptuo.WebStack.Http.Keys;
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
    public static class _HttpHeaderExtensions
    {
        ///// <summary>
        ///// Sets header 'Content-type'.
        ///// </summary>
        //public static IHttpResponse HeaderContentType(this IHttpResponse response, HttpMediaType mediaType)
        //{
        //    return response.Header("Content-type", mediaType);
        //}

        ///// <summary>
        ///// Sets header 'Content-type'.
        ///// </summary>
        //public static HttpMediaType HeaderContentType(this IHttpResponse response)
        //{
        //    return response.HeaderValue<HttpMediaType>("Content-type", null);
        //}

        /// <summary>
        /// Gets request header 'Content-type'.
        /// </summary>
        public static HttpMediaType HeaderContentType(this HttpRequest httpRequest)
        {
            return httpRequest.Header<HttpMediaType>(RequestKey.Header.ContentType, null);
        }

        /// <summary>
        /// Gets request header 'Accept'.
        /// </summary>
        public static IEnumerable<HttpMediaType> HeaderAcceptAll(this HttpRequest httpRequest)
        {
            return httpRequest.Header<IEnumerable<HttpMediaType>>(RequestKey.Header.Accept, Enumerable.Empty<HttpMediaType>());
        }

        /// <summary>
        /// Gets (first value from) request header 'Accept'.
        /// </summary>
        public static HttpMediaType HeaderAccept(this HttpRequest httpRequest)
        {
            return httpRequest.HeaderAcceptAll().FirstOrDefault();
        }
    }
}
