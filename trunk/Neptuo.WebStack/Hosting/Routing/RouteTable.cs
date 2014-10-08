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
        private PatternParser parser;

        public RouteTable(IRouteParameterCollection parameterCollection)
        {
            parser = new PatternParser(parameterCollection);
        }

        public IRouteTable Map(RoutePattern routePattern, IRouteHandler routeHandler)
        {
            Guard.NotNull(routePattern, "routePattern");
            Guard.NotNull(routeHandler, "routeHandler");

            if (routePattern.HasProtocol)
            {

            }
            else if (routePattern.HasDomain)
            {

            }
            else
            {
                // Parse routePattern.VirtualPath into segments (if needed).
                // ;
                List<RouteSegment> segments;
                if (parser.TryBuildUp(routePattern.VirtualPath, out segments))
                {
                    RouteSegment routeSegment = pathTree;
                    foreach (RouteSegment partSegment in segments)
                        routeSegment = routeSegment.Include(partSegment);
                 
                    //routeSegment.PipelineFactory = pipelineFactory;
                }

                return this;
            }

            throw new NotSupportedException();
        }

        public IRouteHandler GetPipeline(IHttpContext httpContext)
        {
            throw new NotImplementedException();
        }



        public PathRouteSegment RootSegment
        {
            get { return pathTree; }
        }
    }
}
