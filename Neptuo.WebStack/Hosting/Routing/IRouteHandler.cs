using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Routing
{
    /// <summary>
    /// Defines invokable handler for processing complete Http request with response.
    /// </summary>
    public interface IRouteHandler
    {
        /// <summary>
        /// Process <paramref name="httpContext"/>.
        /// </summary>
        /// <param name="httpContext">Current Http context.</param>
        Task HandlerAsync(IHttpContext httpContext);
    }
}
