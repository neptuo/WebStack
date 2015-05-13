using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Routing
{
    /// <summary>
    /// Common route table extensions
    /// </summary>
    public static class _RouteTableExtensions
    {
        /// <summary>
        /// Maps <paramref name="target"/> to virtual path <paramref name="virtualPath" />.
        /// </summary>
        /// <param name="virtualPath">Virtual path to regiter <paramref name="target"/> on.</param>
        /// <param name="target">Route target.</param>
        /// <rereturns>Self (for fluency).</rereturns>
        public static IRouteTable MapVirtualPath(this IRouteTable routeTable, string virtualPath, object target)
        {
            Ensure.NotNull(routeTable, "routeTable");
            return routeTable.Map(
                routeTable.UrlBuilder().VirtualPath(virtualPath).ToUrl(), 
                target
            );
        }
    }
}
