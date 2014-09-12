using Neptuo.WebStack.Hosting.Pipelines;
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
        /// <param name="routePattern"></param>
        /// <param name="pipelineFactory"></param>
        /// <rereturns>Self (for fluency).</rereturns>
        IRouteTable Map(RoutePattern routePattern, IPipelineFactory pipelineFactory);

        /// <summary>
        /// Finds pipeline for <paramref name="httpContext"/>.
        /// </summary>
        /// <param name="httpContext">Context describing request and response.</param>
        /// <returns>Pipeline registered for <paramref name="httpContext"/>.</returns>
        IPipeline GetPipeline(IHttpContext httpContext);
    }
}
