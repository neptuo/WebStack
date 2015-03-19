using Neptuo.Templates.Compilation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Templates.Hosting
{
    /// <summary>
    /// Templates extensions for <see cref="EngineEnvironment"/>.
    /// </summary>
    public static class _EnvironmentExtensions
    {
        #region IViewService

        /// <summary>
        /// Registers singleton view service.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <param name="viewService">View service.</param>
        /// <returns><paramref name="environment"/>.</returns>
        public static EngineEnvironment UseViewService(this EngineEnvironment environment, IViewService viewService)
        {
            Ensure.NotNull(environment, "environment");
            Ensure.NotNull(viewService, "viewService");
            return environment.Use<IViewService>(viewService);
        }

        /// <summary>
        /// Tries to retrieve view service.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <returns>Registered view service.</returns>
        public static IViewService WithViewService(this EngineEnvironment environment)
        {
            Ensure.NotNull(environment, "environment");
            return environment.With<IViewService>();
        }

        #endregion
    }
}
