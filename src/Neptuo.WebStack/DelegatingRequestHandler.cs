using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack
{
    /// <summary>
    /// Request handler implementation that goes throught colletion 
    /// of registered handler until first one returns true (handles the request).
    /// The order of executing registered handlers is in FIFO style.
    /// </summary>
    public class DelegatingRequestHandler : IRequestHandler
    {
        private readonly List<IRequestHandler> handlers = new List<IRequestHandler>();

        /// <summary>
        /// Registers handlers in <paramref name="handlers"/>.
        /// </summary>
        /// <param name="handlers">Handlers to register.</param>
        public DelegatingRequestHandler(IEnumerable<IRequestHandler> handlers)
        {
            Guard.NotNull(handlers, "handlers");
            this.handlers.AddRange(handlers);
        }

        /// <summary>
        /// Registers handlers in <paramref name="handlers"/>.
        /// </summary>
        /// <param name="handlers">Handlers to register.</param>
        public DelegatingRequestHandler(params IRequestHandler[] handlers)
        {
            Guard.NotNull(handlers, "handlers");
            this.handlers.AddRange(handlers);
        }

        public async Task<bool> TryHandleAsync(IHttpContext httpContext)
        {
            foreach (IRequestHandler handler in handlers)
            {
                if (await handler.TryHandleAsync(httpContext))
                    return true;
            }

            return false;
        }
    }
}
