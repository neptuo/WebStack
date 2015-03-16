using Neptuo.WebStack.Routing.Segments;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptuo.Collections.Specialized;
using Neptuo.Activators;

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
            IReadOnlyUrl url = httpContext.Request().Url();
            IKeyValueCollection routeValues = httpContext.Request().RouteValues();

            // Try to find target in route table.
            object target;
            if (routeTable.TryGetTarget(url, routeValues, out target))
            {
                IRequestHandler requestHandler = target as IRequestHandler;

                // If target is not request handler, try activator.
                if (requestHandler == null)
                {
                    IActivator<IRequestHandler> activator = target as IActivator<IRequestHandler>;
                    if (activator != null)
                        requestHandler = activator.Create();
                }

                // Otherwise try activator with HTTP context.
                if (requestHandler == null)
                {
                    IActivator<IRequestHandler, IHttpContext> contextActivator = target as IActivator<IRequestHandler, IHttpContext>;
                    if (contextActivator != null)
                        requestHandler = contextActivator.Create(httpContext);
                }

                // If request handler was found, execute it.
                if (requestHandler != null)
                    return requestHandler.TryHandleAsync(httpContext);

                // Otherwise, serialize target as object.
                httpContext.Response().OutputWriter().Write(target);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
