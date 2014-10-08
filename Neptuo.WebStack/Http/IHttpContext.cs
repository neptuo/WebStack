using Neptuo.Collections.Specialized;
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
        IReadOnlyKeyValueCollection Values { get; }

        ///// <summary>
        ///// Http request.
        ///// </summary>
        //IHttpRequest Request { get; }

        ///// <summary>
        ///// Http response.
        ///// </summary>
        //IHttpResponse Response { get; }

        ///// <summary>
        ///// Resolves url starting with ~/...
        ///// </summary>
        ///// <param name="appRelativeUrl">Application relative url.</param>
        ///// <returns>Absolute url.</returns>
        //string ResolveUrl(string appRelativeUrl);
    }

    public static class HttpContextExtensions
    {
        public static IHttpRequest Request(this IHttpContext httpContext)
        {
            return httpContext.Values.Get<IHttpRequest>("Request");
        }

        public static IHttpResponse Response(this IHttpContext httpContext)
        {
            return httpContext.Values.Get<IHttpResponse>("Response");
        }

        public static string ResolveUrl(this IHttpContext httpContext, string relativeUrl)
        {
            throw new NotImplementedException();
        }
    }
}
