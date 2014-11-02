using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting
{
    /// <summary>
    /// Defines invokable handler for processing complete Http request with response.
    /// </summary>
    public interface IRequestHandler
    {
        /// <summary>
        /// Process <paramref name="httpContext"/>.
        /// </summary>
        /// <param name="httpContext">Current Http context.</param>
        /// <returns>
        /// If returns <c>true</c>, this handler is able to process request described in <paramref name="httpContext"/>;
        /// if returns <c>false</c>, request processing should be delegated to the next handler.
        /// </returns>
        Task<bool> HandleAsync(IHttpContext httpContext);
    }
}
