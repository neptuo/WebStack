using Neptuo.Collections.Specialized;
using Neptuo.WebStack.Http;
using Neptuo.WebStack.Routing.Segments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Routing
{
    public class TreeRouteTable : IRouteTable
    {
        private static UrlBuilderSupportedPart supportedPart = UrlBuilderSupportedPart.Schema | UrlBuilderSupportedPart.Host | UrlBuilderSupportedPart.VirtualPath;

        private readonly SchemaSegment schemaTree;
        private readonly HostRouteSegment hostTree;
        private readonly VirtualPathRouteSegment virtualPathTree;
        private readonly PatternParser parser;

        public TreeRouteTable(IRouteParameterCollection parameterCollection)
        {
            schemaTree = new SchemaSegment();
            hostTree = new HostRouteSegment();
            virtualPathTree = new VirtualPathRouteSegment();
            parser = new PatternParser(parameterCollection);
        }

        public TreeRouteTable()
            : this(Engine.Environment.WithParameterCollection())
        { }
        
        public IUrlBuilder UrlBuilder()
        {
            return new UrlBuilder(supportedPart);
        }

        public IRouteTable Map(IReadOnlyUrl routePattern, object target)
        {
            Ensure.NotNull(routePattern, "routePattern");
            Ensure.NotNull(target, "target");

            if (routePattern.HasSchema)
                IntegrateUrl(routePattern.ToString("SHP"), virtualPathTree, target);
            else if (routePattern.HasHost)
                IntegrateUrl(routePattern.ToString("HP"), virtualPathTree, target);
            else if (routePattern.HasVirtualPath)
                IntegrateUrl(routePattern.VirtualPath, virtualPathTree, target);
            else
                throw Ensure.Exception.NotSupported();

            return this;
        }

        private void IntegrateUrl(string url, RouteSegment rootSegment, object target)
        {
            List<RouteSegment> segments;
            if (parser.TryBuildUp(url, out segments))
            {
                RouteSegment routeSegment = rootSegment.TryInclude(segments[0]);
                for (int i = 1; i < segments.Count; i++)
                {
                    RouteSegment partSegment = segments[i];
                    routeSegment = routeSegment.Append(partSegment);
                }

                routeSegment.Target = target;
            }
        }

        public bool TryGetTarget(IReadOnlyUrl url, IKeyValueCollection routeValues, out object target)
        {
            if (url.HasSchema)
            {
                string hostUrl = url.ToString();
                target = schemaTree.ResolveUrl(hostUrl, routeValues);
                if (target != null)
                    return true;
            }

            if (url.HasHost)
            {
                string hostUrl = url.ToString("HP");
                target = hostTree.ResolveUrl(hostUrl, routeValues);
                if (target != null)
                    return true;
            }

            if (url.HasVirtualPath)
            {
                string virtualPath = url.VirtualPath;
                target = virtualPathTree.ResolveUrl(virtualPath, routeValues);
                if (target != null)
                    return true;
            }

            target = null;
            return false;
        }
    }
}
