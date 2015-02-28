using Neptuo.Collections.Specialized;
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
        /// Returns custom values collection from <paramref name="httpContext"/>.
        /// </summary>
        /// <param name="httpContext">Target HTTP context.</param>
        /// <returns>Custom values collection from <paramref name="httpContext"/>.</returns>
        public static IKeyValueCollection CustomValues(this IHttpContext httpContext)
        {
            Guard.NotNull(httpContext, "httpContext");
            return httpContext.With<IKeyValueCollection>();
        }
    }
}
