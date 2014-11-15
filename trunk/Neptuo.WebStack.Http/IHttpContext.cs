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
    /// Describes whole HTTP context.
    /// </summary>
    public interface IHttpContext : IDisposable
    {
        /// <summary>
        /// Collection of supported values.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IKeyValueCollection Values { get; }

        /// <summary>
        /// Event fired when disposing HTTP context.
        /// </summary>
        event Action OnDisposing;
    }

    /// <summary>
    /// Common extensions for <see cref="IHttpContext"/>.
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// HTTP request.
        /// </summary>
        public static IHttpRequest Request(this IHttpContext httpContext)
        {
            Guard.NotNull(httpContext, "httpContext");
            return httpContext.Values.Get<IHttpRequest>(ContextKey.Request);
        }

        /// <summary>
        /// HTTP response.
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
        /// Gets collection for storring custom HTTP context values.
        /// </summary>
        public static IKeyValueCollection Params(this IHttpContext httpContext)
        {
            Guard.NotNull(httpContext, "httpContext");
            IKeyValueCollection collection;
            if(!httpContext.Values.TryGet<IKeyValueCollection>("Params", out collection))
            {
                collection = new KeyValueCollection();
                httpContext.Values.Set("Params", collection);
            }

            return collection;
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
