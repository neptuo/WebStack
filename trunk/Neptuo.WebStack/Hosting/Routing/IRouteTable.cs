using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Routing
{
    /// <summary>
    /// Holds routes and mapped pipelines.
    /// </summary>
    public interface IRouteTable
    {
        /// <summary>
        /// Maps <paramref name="pipelineFactory"/> to <paramref name="routePattern"/>.
        /// </summary>
        /// <param name="routePattern">Pattern to regiter <paramref name="routeHandler"/> on.</param>
        /// <param name="routeHandler">Handler for handling requests.</param>
        /// <rereturns>Self (for fluency).</rereturns>
        IRouteTable Map(Url routePattern, IRouteHandler routeHandler);

        /// <summary>
        /// Finds route handler for <paramref name="httpContext"/>.
        /// </summary>
        /// <param name="httpContext">Context describing request and response.</param>
        /// <returns>Route handler registered for <paramref name="httpContext"/>.</returns>
        IRouteHandler GetRouteHandler(IHttpContext httpContext);
    }
}
