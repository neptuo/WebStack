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
        /// <param name="routePattern">Pattern to regiter <paramref name="requestHandler"/> on.</param>
        /// <param name="requestHandler">Handler for handling requests.</param>
        /// <rereturns>Self (for fluency).</rereturns>
        IRouteTable Map(Url routePattern, IRequestHandler requestHandler);
    }
}
