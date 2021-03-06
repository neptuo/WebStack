﻿using Neptuo.WebStack.Http;
using Neptuo.WebStack.Routing;
using Neptuo.WebStack.Services.Hosting.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Services.Hosting
{
    /// <summary>
    /// Extensions for mapping routes for services.
    /// </summary>
    public static class _RouteTableExtensions
    {
        /// <summary>
        /// Maps types in <paramref name="assemblies"/> decorated with <see cref="RouteAttribute"/> using <see cref="CodeDomPipelineFactory"/>.
        /// </summary>
        /// <param name="routeTable">Route table.</param>
        /// <param name="assemblies">List of assemblies to search.</param>
        /// <returns><paramref name="routeTable"/>.</returns>
        public static IRouteTable MapServices(this IRouteTable routeTable, params Assembly[] assemblies)
        {
            Ensure.NotNull(routeTable, "routeTable");
            Ensure.NotNull(assemblies, "assemblies");

            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    RouteAttribute attribute = type.GetCustomAttribute<RouteAttribute>();
                    if (attribute != null)
                        routeTable.Map(routeTable.UrlBuilder().FromUrl(attribute.Url), new CodeDomServiceHandlerFactory(type));
                }
            }

            return routeTable;
        }

        /// <summary>
        /// Maps <paramref name="handlerType"/> to route defined by attribute <see cref="RouteAttribute"/> using <see cref="CodeDomPipelineFactory"/>.
        /// If not defined on type <paramref name="handlerType"/>, do nothing.
        /// </summary>
        /// <param name="routeTable">Route table.</param>
        /// <param name="handlerType">Service handler type to register (decorated with <see cref="RouteAttribute"/>).</param>
        /// <returns><paramref name="routeTable"/>.</returns>
        public static IRouteTable MapService(this IRouteTable routeTable, Type handlerType)
        {
            RouteAttribute attribute = handlerType.GetCustomAttribute<RouteAttribute>();
            if (attribute != null)
                return routeTable.MapService(routeTable.UrlBuilder().FromUrl(attribute.Url), handlerType);

            return routeTable;
        }

        /// <summary>
        /// Maps <paramref name="handlerType"/> to route defined by <paramref name="url"/> using <see cref="CodeDomPipelineFactory"/>.
        /// </summary>
        /// <param name="routeTable">Route table.</param>
        /// <param name="url">Route for <paramref name="handlerType"/>.</param>
        /// <param name="handlerType">Service handler type to register (decorated with <see cref="RouteAttribute"/>).</param>
        /// <returns><paramref name="routeTable"/>.</returns>
        public static IRouteTable MapService(this IRouteTable routeTable, IReadOnlyUrl url, Type handlerType)
        {
            return routeTable.Map(url, new CodeDomServiceHandlerFactory(handlerType));
        }
    }
}
