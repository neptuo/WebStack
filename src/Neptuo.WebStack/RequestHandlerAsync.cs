using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack
{
    /// <summary>
    /// Base class with some util methods.
    /// </summary>
    public abstract class RequestHandlerAsync : IRequestHandler
    {
        /// <summary>
        /// Process <paramref name="httpContext"/>.
        /// </summary>
        /// <param name="httpContext">Current HTTP context.</param>
        /// <returns><c>true</c> if request was handled; <c>false</c> to process request by next handler.</returns>
        protected abstract Task<bool> TryHandleAsync(IHttpContext httpContext);

        Task<bool> IRequestHandler.TryHandleAsync(IHttpContext httpContext)
        {
            return TryHandleAsync(httpContext);
        }
    }
}
