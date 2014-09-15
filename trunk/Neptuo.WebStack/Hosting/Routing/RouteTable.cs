using Neptuo.WebStack.Hosting.Pipelines;
using Neptuo.WebStack.Hosting.Routing.Segments;
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
        private PathRouteSegment pathTree = new PathRouteSegment();

        public IRouteTable Map(RoutePattern routePattern, IPipelineFactory pipelineFactory)
        {
            Guard.NotNull(routePattern, "routePattern");
            Guard.NotNull(pipelineFactory, "pipelineFactory");

            if (routePattern.HasProtocol)
            {

            }
            else if (routePattern.HasDomain)
            {

            }
            else
            {
                // Parse routePattern.VirtualPath into segments (if needed).
                // 

                pathTree.IncludeUrl(routePattern.VirtualPath, pipelineFactory);
            }

            throw new NotImplementedException();
        }

        public IPipeline GetPipeline(IHttpContext httpContext)
        {
            throw new NotImplementedException();
        }
    }
}
