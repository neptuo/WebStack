using Neptuo.Collections.Specialized;
using Neptuo.FeatureModels;
using Neptuo.WebStack.Http.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Common extensions for <see cref="IHttpContext"/>.
    /// </summary>
    public static class _HttpContextExtensions
    {
        /// <summary>
        /// Returns custom values collection from <paramref name="httpContext"/>.
        /// </summary>
        /// <param name="httpContext">Target HTTP context.</param>
        /// <returns>Custom values collection from <paramref name="httpContext"/>.</returns>
        public static IKeyValueCollection CustomValues(this IHttpContext httpContext)
        {
            Guard.NotNull(httpContext, "httpContext");
            return httpContext.With<IKeyValueCollection>();
        }

        /// <summary>
        /// Returns extensible HTTP request from <paramref name="httpContext"/>.
        /// </summary>
        /// <param name="httpContext">Target HTTP context.</param>
        /// <returns>Extensible HTTP request from <paramref name="httpContext"/>.</returns>
        public static HttpRequest Request(this IHttpContext httpContext)
        {
            Guard.NotNull(httpContext, "httpContext");

            HttpRequest httpRequest;
            if (!httpContext.CustomValues().TryGet(RequestKey.Root, out httpRequest))
                httpContext.CustomValues().Set(RequestKey.Root, httpRequest = new HttpRequest(httpContext));

            return httpRequest;
        }

        /// <summary>
        /// Returns extensible HTTP request from <paramref name="httpContext"/>.
        /// </summary>
        /// <param name="httpContext">Target HTTP context.</param>
        /// <returns>Extensible HTTP request from <paramref name="httpContext"/>.</returns>
        public static HttpResponse Response(this IHttpContext httpContext)
        {
            Guard.NotNull(httpContext, "httpContext");

            HttpResponse httpResponse;
            if (!httpContext.CustomValues().TryGet(ResponseKey.Root, out httpResponse))
                httpContext.CustomValues().Set(ResponseKey.Root, httpResponse = new HttpResponse(httpContext));

            return httpResponse;
        }
    }
}
