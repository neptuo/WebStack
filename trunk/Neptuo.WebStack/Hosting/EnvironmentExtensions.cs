﻿using Neptuo.WebStack.Hosting.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting
{
    /// <summary>
    /// WebStack extensions for <see cref="EngineEnvironment"/>.
    /// </summary>
    public static class EnvironmentExtensions
    {
        #region Root handler

        /// <summary>
        /// Registers application root handler for HTTP request.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <param name="requestHandler">Handler for handling all requests.</param>
        /// <returns><paramref name="environment"/>.</returns>
        public static EngineEnvironment UseRootRequestHandler(this EngineEnvironment environment, IRequestHandler requestHandler)
        {
            Guard.NotNull(environment, "environment");
            Guard.NotNull(requestHandler, "requestHandler");
            return environment.Use<IRequestHandler>(requestHandler);
        }

        /// <summary>
        /// Tries to retrieve root handler for HTTP request.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <returns>Root handler for HTTP request.</returns>
        public static IRequestHandler WithRootRequestHandler(this EngineEnvironment environment)
        {
            Guard.NotNull(environment, "environment");
            return environment.With<IRequestHandler>();
        }

        #endregion

        #region Route table

        /// <summary>
        /// Registers singleton route table.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <param name="routeTable">Route table instance.</param>
        /// <returns><paramref name="environment"/>.</returns>
        public static EngineEnvironment UseRouteTable(this EngineEnvironment environment, IRouteTable routeTable)
        {
            Guard.NotNull(environment, "environment");
            Guard.NotNull(routeTable, "routeTable");
            return environment.Use<IRouteTable>(routeTable);
        }

        /// <summary>
        /// Registers singleton route table with default implementation.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <returns><paramref name="environment"/>.</returns>
        public static EngineEnvironment UseRouteTable(this EngineEnvironment environment)
        {
            Guard.NotNull(environment, "environment");
            return UseRouteTable(environment, new RouteRequestHandler(environment.WithParameterCollection()));
        }

        /// <summary>
        /// Registers singleton route table and use <paramref name="mapper"/> to initialize routes.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <param name="mapper">Route mapper/initializer.</param>
        /// <returns><paramref name="environment"/>.</returns>
        public static EngineEnvironment UseRouteTable(this EngineEnvironment environment, Action<IRouteTable> mapper)
        {
            Guard.NotNull(environment, "environment");
            Guard.NotNull(mapper, "mapper");

            RouteRequestHandler routeTable = new RouteRequestHandler(environment.WithParameterCollection());
            mapper(routeTable);
            return UseRouteTable(environment, routeTable);
        }

        /// <summary>
        /// Tries to retrieve route table.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <returns>Route table instance.</returns>
        public static IRouteTable WithRouteTable(this EngineEnvironment environment)
        {
            Guard.NotNull(environment, "environment");
            return environment.With<IRouteTable>();
        }

        #endregion

        #region Parameter collection

        /// <summary>
        /// Registers single route parameter collection and use <paramref name="mapper"/> to initializes parameters.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <param name="mapper">Parameter mapper/initializer.</param>
        /// <returns><paramref name="environment"/>.</returns>
        public static EngineEnvironment UseParameterCollection(this EngineEnvironment environment, Action<IRouteParameterCollection> mapper)
        {
            Guard.NotNull(environment, "environment");
            Guard.NotNull(mapper, "mapper");
            RouteParameterCollection parameterCollection = new RouteParameterCollection();
            mapper(parameterCollection);
            return environment.Use<IRouteParameterCollection>(parameterCollection);
        }

        /// <summary>
        /// Tries to retrieve route parameter collection.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <returns>Route parameter collection.</returns>
        public static IRouteParameterCollection WithParameterCollection(this EngineEnvironment environment)
        {
            Guard.NotNull(environment, "environment");
            return environment.With<IRouteParameterCollection>();
        }

        #endregion
    }
}