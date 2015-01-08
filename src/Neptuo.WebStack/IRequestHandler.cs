using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack
{
    /// <summary>
    /// Defines invokable handler for processing complete HTTP request with response.
    /// </summary>
    public interface IRequestHandler
    {
        /// <summary>
        /// Process <paramref name="httpRequest"/>.
        /// </summary>
        /// <param name="httpRequest">Current Http context.</param>
        /// <returns>
        /// If returns <c>null</c>, request processing should be delegated to the next handler.
        /// If returns any non-null response, this handler handled the request described in <paramref name="httpRequest"/>;
        /// </returns>
        Task<IHttpResponse> TryHandleAsync(IHttpRequest httpRequest);
    }
}
