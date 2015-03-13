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
        /// Process <paramref name="httpContext"/>.
        /// </summary>
        /// <param name="httpContext">Current HTTP context.</param>
        /// <returns><c>true</c> if request was handled; <c>false</c> to process request by next handler.</returns>
        Task<bool> TryHandleAsync(IHttpContext httpContext);
    }
}
