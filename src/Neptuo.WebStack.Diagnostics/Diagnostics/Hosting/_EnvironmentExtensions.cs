using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Diagnostics.Hosting
{
    /// <summary>
    /// Exceptions extensions for <see cref="EngineEnvironment"/>
    /// </summary>
    public static class _EnvironmentExtensions
    {
        #region IExceptionTable

        /// <summary>
        /// Registers singleton exception table.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <param name="requestHandler">Exception table.</param>
        /// <returns><paramref name="environment"/>.</returns>
        public static EngineEnvironment UseExceptionTable(this EngineEnvironment environment, IExceptionTable exceptionTable)
        {
            Ensure.NotNull(environment, "environment");
            return environment.Use<IExceptionTable>(exceptionTable);
        }

        /// <summary>
        /// Tries to retrieve exception table.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <returns>Exception table.</returns>
        public static IExceptionTable WithExceptionTable(this EngineEnvironment environment)
        {
            Ensure.NotNull(environment, "environment");
            return environment.With<IExceptionTable>();
        }

        #endregion
    }
}
