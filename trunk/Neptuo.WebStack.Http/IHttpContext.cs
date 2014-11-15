using Neptuo.Collections.Specialized;
using Neptuo.WebStack.Http.Keys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Describes whole Http context.
    /// </summary>
    public interface IHttpContext
    {
        /// <summary>
        /// Collection of supported values.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IKeyValueCollection Values { get; }
    }

    /// <summary>
    /// Common extensions for <see cref="IHttpContext"/>.
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Http request.
        /// </summary>
        public static IHttpRequest Request(this IHttpContext httpContext)
        {
            Guard.NotNull(httpContext, "httpContext");
            return httpContext.Values.Get<IHttpRequest>(ContextKey.Request);
        }

        /// <summary>
        /// Http response.
        /// </summary>
        public static IHttpResponse Response(this IHttpContext httpContext)
        {
            Guard.NotNull(httpContext, "httpContext");
            return httpContext.Values.Get<IHttpResponse>(ContextKey.Response);
        }

        /// <summary>
        /// Gets builder for URL addresses.
        /// </summary>
        public static IUrlBuilder UrlBuilder(this IHttpContext httpContext)
        {
            Guard.NotNull(httpContext, "httpContext");
            return httpContext.Values.Get<IUrlBuilder>(ContextKey.UrlBuilder);
        }

        /// <summary>
        /// Resolves url starting with ~/...
        /// </summary>
        /// <param name="appRelativeUrl">Application relative url.</param>
        /// <returns>Absolute url.</returns>
        public static string ResolveUrl(this IHttpContext httpContext, string appRelativeUrl)
        {
            Guard.NotNull(httpContext, "httpContext");
            throw new NotImplementedException();
        }
    }
}
