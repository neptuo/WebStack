using Neptuo.WebStack.Exceptions;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack
{
    /// <summary>
    /// Exceptions extensions for <see cref="IHttpContext"/>.
    /// </summary>
    public static class _HttpContextExtensions
    {
        /// <summary>
        /// Returns exception stack for <paramref name="httpContext"/>.
        /// </summary>
        /// <param name="httpContext">Target HTTP context.</param>
        /// <returns>Exception stack for <paramref name="httpContext"/>.</returns>
        public static ExceptionStack Exceptions(this IHttpContext httpContext)
        {
            Ensure.NotNull(httpContext, "httpContext");

            ExceptionStack stack;
            if (!httpContext.CustomValues().TryGet("Exceptions", out stack))
                httpContext.CustomValues().Set("Exceptions", stack = new ExceptionStack());

            return stack;
        }
    }
}
