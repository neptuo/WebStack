using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// WebStack extensions for <see cref="EngineEnvironment"/>.
    /// </summary>
    public static class EnvironmentExtensions
    {
        #region Url builder

        /// <summary>
        /// Registers application URL builder.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <param name="urlBuilder">Builder for URL addresses.</param>
        /// <returns><paramref name="environment"/>.</returns>
        public static EngineEnvironment UseRootRequestHandler(this EngineEnvironment environment, IUrlBuilder urlBuilder)
        {
            Guard.NotNull(environment, "environment");
            Guard.NotNull(urlBuilder, "urlBuilder");
            return environment.Use<IUrlBuilder>(urlBuilder);
        }

        /// <summary>
        /// Tries to retrieve URL builder.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <returns>URL builder.</returns>
        public static IUrlBuilder WithUrlBuilder(this EngineEnvironment environment)
        {
            Guard.NotNull(environment, "environment");
            return environment.With<IUrlBuilder>();
        }

        #endregion
    }
}
