using Neptuo.FeatureModels;
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
        /// Returns raw HTTP request from <paramref name="httpContext"/>.
        /// </summary>
        /// <param name="httpContext">Target HTTP context.</param>
        /// <returns>Raw HTTP request from <paramref name="httpContext"/>.</returns>
        public static IHttpRequestMessage RequestMessage(this IHttpContext httpContext)
        {
            Guard.NotNull(httpContext, "httpContext");
            return httpContext.With<IHttpRequestMessage>();
        }

        /// <summary>
        /// Returns raw HTTP response from <paramref name="httpContext"/>.
        /// </summary>
        /// <param name="httpContext">Target HTTP context.</param>
        /// <returns>Raw HTTP response from <paramref name="httpContext"/>.</returns>
        public static IHttpResponseMessage ResponseMessage(this IHttpContext httpContext)
        {
            Guard.NotNull(httpContext, "httpContext");
            return httpContext.With<IHttpResponseMessage>();
        }
    }
}
