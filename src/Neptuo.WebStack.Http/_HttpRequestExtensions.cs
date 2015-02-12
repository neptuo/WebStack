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
    /// Common extensions for <see cref="IHttpRequest"/>.
    /// </summary>
    public static class _HttpRequestExtensions
    {
        public static HttpMethod Method(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");

            HttpMethod method;
            if (!request.CustomValues.TryGet(RequestKey.Method, out method))
            {
                method = Converts.To<string, HttpMethod>(request.RawValues.Method);
                request.CustomValues.Set(RequestKey.Method, method);
            }

            return method;
        }

        /// <summary>
        /// Returns <c>true</c> if Http method equals to Get; returns <c>false</c> otherwise.
        /// </summary>
        public static bool IsMethodGet(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Method() == HttpMethod.Get;
        }

        /// <summary>
        /// Returns <c>true</c> if Http method equals to Post; returns <c>false</c> otherwise.
        /// </summary>
        public static bool IsMethodPost(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Method() == HttpMethod.Post;
        }

        /// <summary>
        /// Returns <c>true</c> if Http method equals to Put; returns <c>false</c> otherwise.
        /// </summary>
        public static bool IsMethodPut(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Method() == HttpMethod.Put;
        }

        /// <summary>
        /// Returns <c>true</c> if Http method equals to Delete; returns <c>false</c> otherwise.
        /// </summary>
        public static bool IsMethodDelete(this IHttpRequest request)
        {
            Guard.NotNull(request, "request");
            return request.Method() == HttpMethod.Delete;
        }

        /// <summary>
        /// Http request headers.
        /// </summary>
        public static T Header<T>(this IHttpRequest request, string headerName, T? defaltValue)
            where T : struct
        {
            Guard.NotNull(request, "request");
            Guard.NotNullOrEmpty(headerName, "headerName");
            return request.Headers().Get<T>(headerName, defaltValue);
        }

        /// <summary>
        /// Http request headers.
        /// </summary>
        public static T Header<T>(this IHttpRequest request, string headerName, T defaltValue)
            where T : class
        {
            Guard.NotNull(request, "request");
            Guard.NotNullOrEmpty(headerName, "headerName");
            return request.Headers().Get<T>(headerName, defaltValue);
        }


        /// <summary>
        /// Gets builder for URL addresses.
        /// </summary>
        public static IUrlBuilder UrlBuilder(this IHttpRequest httpRequest)
        {
            Guard.NotNull(httpRequest, "httpRequest");
            return httpRequest.DependencyProvider().Resolve<IUrlBuilder>();
        }
    }
}
