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
        private static UrlBuilderSupportedPart supportedPart = UrlBuilderSupportedPart.Schema | UrlBuilderSupportedPart.Host | UrlBuilderSupportedPart.VirtualPath;

        private readonly StaticRouteSegment schemaTree;
        private readonly HostRouteSegment hostTree;
        private readonly VirtualPathRouteSegment virtualPathTree;
        private readonly PatternParser parser;

        public RouteRequestHandler(IRouteParameterCollection parameterCollection)
        {
            schemaTree = null;
            hostTree = new HostRouteSegment();
            virtualPathTree = new VirtualPathRouteSegment();
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
            else if (routePattern.HasHost)
            {

            }
            else
            {
                // Parse routePattern.VirtualPath into segments (if needed).
                List<RouteSegment> segments;
                if (parser.TryBuildUp(routePattern.VirtualPath, out segments))
                {
                    RouteSegment routeSegment = virtualPathTree.TryInclude(segments[0]);
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
            IReadOnlyUrl requestUrl = httpContext.Request().Url();
            if (requestUrl.HasSchema)
            {
                //TODO: Implement URL match from schema.
            }

            if (requestUrl.HasHost)
            {
                string hostUrl = httpContext.Request().Url().ToString("HP");
                IRequestHandler requestHandler = hostTree.ResolveUrl(hostUrl);
                if (requestHandler != null)
                    return requestHandler.TryHandleAsync(httpContext);
            }

            if (requestUrl.HasVirtualPath)
            {
                string virtualPath = httpContext.Request().Url().VirtualPath;
                IRequestHandler requestHandler = virtualPathTree.ResolveUrl(virtualPath);
                if (requestHandler != null)
                    return requestHandler.TryHandleAsync(httpContext);
            }

            return Task.FromResult(false);
        }

        /// <summary>
        /// TODO: Very temporal.
        /// </summary>
        public VirtualPathRouteSegment PathTree
        {
            get { return virtualPathTree; }
        }
    }
}
