using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack
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
    }
}
