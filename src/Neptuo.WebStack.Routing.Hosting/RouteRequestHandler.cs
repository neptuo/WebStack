using Neptuo.WebStack.Routing.Segments;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Routing.Hosting
{
    /// <summary>
    /// Route table request handler.
    /// </summary>
    public class RouteRequestHandler : IRequestHandler
    {
        private IRouteTable routeTable;

        public RouteRequestHandler(IRouteTable routeTable)
        {
            Ensure.NotNull(routeTable, "routeTable");
            this.routeTable = routeTable;
        }

        public RouteRequestHandler()
            : this(Engine.Environment.WithRouteTable())
        { }

        public Task<bool> TryHandleAsync(IHttpContext httpContext)
        {
            object target;
            if (routeTable.TryGetTarget(httpContext, out target))
            {
                IRequestHandler requestHandler = target as IRequestHandler;
                if (requestHandler != null)
                    return requestHandler.TryHandleAsync(httpContext);

                //TODO: Log not supported route target.
            }

            return Task.FromResult(false);
        }
    }
}
