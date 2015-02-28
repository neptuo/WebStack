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
    /// Extensions for type safe access to HTTP headers on both HTTP request and response.
    /// </summary>
    public static class _HttpHeaderExtensions_MediaType
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
        public static HttpMediaType ContentType(this HttpRequestHeaderCollection httpHeaders)
        {
            return httpHeaders.Get<HttpMediaType>(RequestKey.Header.ContentType, null);
        }

        /// <summary>
        /// Gets request header 'Accept'.
        /// </summary>
        public static IEnumerable<HttpMediaType> Accept(this HttpRequestHeaderCollection httpHeaders)
        {
            return httpHeaders.Get<IEnumerable<HttpMediaType>>(RequestKey.Header.Accept, Enumerable.Empty<HttpMediaType>());
        }
    }
}
