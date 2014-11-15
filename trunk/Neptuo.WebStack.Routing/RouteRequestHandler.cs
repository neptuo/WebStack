using Neptuo.WebStack.Routing.Segments;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Routing
{
    /// <summary>
    /// 'Tree' route table implementation.
    /// </summary>
    public class RouteRequestHandler : IRouteTable, IRequestHandler
    {
        private static UrlBuilderSupportedPart supportedPart = UrlBuilderSupportedPart.Schema | UrlBuilderSupportedPart.Domain | UrlBuilderSupportedPart.VirtualPath;

        private readonly PathRouteSegment pathTree;
        private readonly PatternParser parser;

        public RouteRequestHandler(IRouteParameterCollection parameterCollection)
        {
            pathTree = new PathRouteSegment();
            parser = new PatternParser(parameterCollection);
        }

        public IUrlBuilder UrlBuilder()
        {
            return new UrlBuilder(supportedPart);
        }

        public IRouteTable Map(IReadOnlyUrl routePattern, IRequestHandler requestHandler)
        {
            Guard.NotNull(routePattern, "routePattern");
            Guard.NotNull(requestHandler, "requestHandler");

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

                    routeSegment.RequestHandler = requestHandler;
                }

                return this;
            }

            throw new NotSupportedException();
        }

        public Task<bool> TryHandleAsync(IHttpContext httpContext)
        {
            string virtualPath = "~" + httpContext.Request().Url().Path;
            IRequestHandler requestHandler = pathTree.ResolveUrl(virtualPath);
            if (requestHandler != null)
                return requestHandler.TryHandleAsync(httpContext);

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
