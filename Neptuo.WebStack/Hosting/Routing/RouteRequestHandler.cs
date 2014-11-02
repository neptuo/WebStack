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
    public class RouteRequestHandler : IRouteTable, IRequestHandler
    {
        private PathRouteSegment pathTree = new PathRouteSegment();
        private PatternParser parser;

        public RouteRequestHandler(IRouteParameterCollection parameterCollection)
        {
            parser = new PatternParser(parameterCollection);
        }

        public IRouteTable Map(Url routePattern, IRequestHandler routeHandler)
        {
            Guard.NotNull(routePattern, "routePattern");
            Guard.NotNull(routeHandler, "routeHandler");

            if (routePattern.HasSchema)
            {

            }
            else if (routePattern.HasDomain)
            {

            }
            else
            {
                // Parse routePattern.VirtualPath into segments (if needed).
                List<RouteSegment> segments;
                if (parser.TryBuildUp(routePattern.VirtualPath, out segments))
                {
                    RouteSegment routeSegment = pathTree.TryInclude(segments[0]);
                    for (int i = 1; i < segments.Count; i++)
                    {
                        RouteSegment partSegment = segments[i];
                        routeSegment = routeSegment.Append(partSegment);
                    }

                    routeSegment.RouteHandler = routeHandler;
                }

                return this;
            }

            throw new NotSupportedException();
        }

        public Task<bool> HandleAsync(IHttpContext httpContext)
        {
            string virtualPath = "~" + httpContext.Request().Url().AbsolutePath;
            IRequestHandler routeHandler = pathTree.ResolveUrl(virtualPath);
            if (routeHandler != null)
                return routeHandler.HandleAsync(httpContext);

            return Task.FromResult(false);
        }

        /// <summary>
        /// TODO: Very temporal.
        /// </summary>
        public PathRouteSegment PathTree
        {
            get { return pathTree; }
        }
    }
}
