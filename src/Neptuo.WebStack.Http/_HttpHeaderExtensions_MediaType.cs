﻿using Neptuo.Collections.Specialized;
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
        /// <summary>
        /// Sets header 'Content-type'.
        /// </summary>
        public static HttpResponseHeaderCollection ContentType(this HttpResponseHeaderCollection httpHeaders, HttpMediaType mediaType)
        {
            Ensure.NotNull(httpHeaders, "httpHeaders");
            return httpHeaders.Set("Content-type", mediaType);
        }

        /// <summary>
        /// Sets header 'Content-type'.
        /// </summary>
        public static HttpMediaType ContentType(this HttpResponseHeaderCollection httpHeaders)
        {
            Ensure.NotNull(httpHeaders, "httpHeaders");
            return httpHeaders.GetOrDefault<HttpMediaType>("Content-type", null);
        }

        /// <summary>
        /// Gets request header 'Content-type'.
        /// </summary>
        public static HttpMediaType ContentType(this HttpRequestHeaderCollection httpHeaders)
        {
            Ensure.NotNull(httpHeaders, "httpHeaders");
            return httpHeaders.GetOrDefault<HttpMediaType>(RequestKey.Header.ContentType, null);
        }

        /// <summary>
        /// Gets request header 'Accept'.
        /// </summary>
        public static IEnumerable<HttpMediaType> Accept(this HttpRequestHeaderCollection httpHeaders)
        {
            Ensure.NotNull(httpHeaders, "httpHeaders");
            return httpHeaders.GetOrDefault<IEnumerable<HttpMediaType>>(RequestKey.Header.Accept, Enumerable.Empty<HttpMediaType>());
        }
    }
}
