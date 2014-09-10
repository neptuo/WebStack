using Neptuo.WebStack.Hosting.Pipelines;
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


    }
}
