using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Resources
{
    /// <summary>
    /// Resource extensions for <see cref="EngineEnvironment"/>.
    /// </summary>
    public static class _EnvironmentExtensions
    {
        #region Route table

        /// <summary>
        /// Registers singleton resource table.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <param name="resourceCollection">Resource table instance.</param>
        /// <returns><paramref name="environment"/>.</returns>
        public static EngineEnvironment UseResourceTable(this EngineEnvironment environment, IResourceCollection resourceCollection)
        {
            Ensure.NotNull(environment, "environment");
            Ensure.NotNull(resourceCollection, "routeTable");
            return environment.Use<IResourceCollection>(resourceCollection);
        }

        /// <summary>
        /// Tries to retrieve resource table.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <returns>Resource table instance.</returns>
        public static IResourceCollection WithResourceTable(this EngineEnvironment environment)
        {
            Ensure.NotNull(environment, "environment");
            return environment.With<IResourceCollection>();
        }

        #endregion

    }
}
