using Neptuo.WebStack.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Routing
{
    /// <summary>
    /// WebStack extensions for <see cref="EngineEnvironment"/>.
    /// </summary>
    public static class _EnvironmentExtensions
    {
        #region Route table

        /// <summary>
        /// Registers singleton route table.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <param name="routeTable">Route table instance.</param>
        /// <param name="mapper">Optional route mapper.</param>
        /// <returns><paramref name="environment"/>.</returns>
        public static EngineEnvironment UseRouteTable(this EngineEnvironment environment, IRouteTable routeTable, Action<IRouteTable> mapper = null)
        {
            Ensure.NotNull(environment, "environment");
            Ensure.NotNull(routeTable, "routeTable");

            if (mapper != null)
                mapper(routeTable);

            return environment.Use<IRouteTable>(routeTable);
        }

        /// <summary>
        /// Registers singleton tree route table.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <param name="mapper">Optional route mapper.</param>
        /// <returns><paramref name="environment"/>.</returns>
        public static EngineEnvironment UseTreeRouteTable(this EngineEnvironment environment, Action<IRouteTable> mapper = null)
        {
            return UseRouteTable(environment, new TreeRouteTable(WithParameterCollection(environment)), mapper);
        }

        /// <summary>
        /// Tries to retrieve route table.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <returns>Route table instance.</returns>
        public static IRouteTable WithRouteTable(this EngineEnvironment environment)
        {
            Ensure.NotNull(environment, "environment");
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
            Ensure.NotNull(environment, "environment");
            Ensure.NotNull(mapper, "mapper");
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
            Ensure.NotNull(environment, "environment");
            return environment.With<IRouteParameterCollection>();
        }

        #endregion
    }
}
