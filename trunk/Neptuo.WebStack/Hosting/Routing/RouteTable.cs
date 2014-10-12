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
                    RouteSegment routeSegment = pathTree.TryInclude(segments[0]);
                    for (int i = 1; i < segments.Count; i++)
                    {
                        RouteSegment partSegment = segments[i];
                        routeSegment = routeSegment.Append(partSegment);
                    }

                    //foreach (RouteSegment partSegment in segments)
                    //    routeSegment = routeSegment.Old_Include(partSegment, true);
                    
                    routeSegment.RouteHandler = routeHandler;
                }

                return this;
            }

            throw new NotSupportedException();
        }

        public IRouteHandler GetRouteHandler(IHttpContext httpContext)
        {
            throw new NotImplementedException();
        }



        public PathRouteSegment RootSegment
        {
            get { return pathTree; }
        }
    }
}
