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
    /// 'Tree' route table implementation.
    /// </summary>
    public class RouteTable : IRouteTable
    {
        public IRouteTable Map(RoutePattern routePattern, IPipelineFactory pipelineFactory)
        {
            throw new NotImplementedException();
        }

        public IPipeline GetPipeline(IHttpContext httpContext)
        {
            throw new NotImplementedException();
        }
    }
}
