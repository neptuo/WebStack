using Neptuo.Collections.Specialized;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Routing
{
    /// <summary>
    /// Common extensions for <see cref="Http.HttpRequest"/> for routing.
    /// </summary>
    public static class _HttRequestExtensions_Routing
    {
        public static IKeyValueCollection RouteValues(this HttpRequest httpRequest)
        {
            Ensure.NotNull(httpRequest, "httpRequest");
            IKeyValueCollection routeValues;
            if (!httpRequest.CustomValues().TryGet("RouteValues", out routeValues))
                httpRequest.CustomValues().Set("RouteValues", routeValues = new KeyValueCollection());

            return routeValues;
        }
    }
}
