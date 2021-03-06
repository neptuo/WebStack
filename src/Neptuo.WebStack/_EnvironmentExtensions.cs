﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack
{
    /// <summary>
    /// WebStack extensions for <see cref="EngineEnvironment"/>.
    /// </summary>
    public static class _EnvironmentExtensions
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
            Ensure.NotNull(environment, "environment");
            Ensure.NotNull(requestHandler, "requestHandler");
            return environment.Use<IRequestHandler>(requestHandler);
        }

        /// <summary>
        /// Tries to retrieve root handler for HTTP request.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <returns>Root handler for HTTP request.</returns>
        public static IRequestHandler WithRootRequestHandler(this EngineEnvironment environment)
        {
            Ensure.NotNull(environment, "environment");
            return environment.With<IRequestHandler>();
        }

        #endregion
    }
}
