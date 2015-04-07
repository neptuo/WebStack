using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Diagnostics.Internals
{
    /// <summary>
    /// Wrapper for request handler into exception handler.
    /// </summary>
    internal class RequestExceptionHandler : IExceptionHandler
    {
        private readonly IRequestHandler requestHandler;

        public RequestExceptionHandler(IRequestHandler requestHandler)
        {
            Ensure.NotNull(requestHandler, "requestHandler");
            this.requestHandler = requestHandler;
        }

        public Task<bool> TryHandleAsync(Exception exception, IHttpContext httpContext)
        {
            return requestHandler.TryHandleAsync(httpContext);
        }
    }
}
