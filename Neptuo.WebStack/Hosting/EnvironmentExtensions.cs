using Neptuo.WebStack.Hosting.Routing;
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
            return UseRouteTable(environment, new RouteTable());
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

            RouteTable routeTable = new RouteTable();
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



        public static EngineEnvironment UseParameterCollection(this EngineEnvironment environment, Action<IRouteParameterCollection> mapper)
        {
            Guard.NotNull(environment, "environment");
            Guard.NotNull(mapper, "mapper");

            throw new NotImplementedException();
        }
    }
}
