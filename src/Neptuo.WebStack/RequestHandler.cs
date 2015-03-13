using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack
{
    /// <summary>
    /// Synchronous version of <see cref="RequestHandlerAsync"/>.
    /// </summary>
    public abstract class RequestHandler : RequestHandlerAsync
    {
        /// <summary>
        /// Process <paramref name="httpContext"/>.
        /// </summary>
        /// <param name="httpContext">Current HTTP context.</param>
        /// <returns><c>true</c> if request was handled; <c>false</c> to process request by next handler.</returns>
        protected abstract bool TryHandle(IHttpContext httpContext);

        protected override sealed Task<bool> TryHandleAsync(IHttpContext httpContext)
        {
            return Task.FromResult(TryHandle(httpContext));
        }
    }
}
