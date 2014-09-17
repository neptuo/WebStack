using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Routing
{
    /// <summary>
    /// Collection of registered route parameters.
    /// These parameter can represent dynamic parts of routes.
    /// </summary>
    public interface IRouteParameterCollection
    {
        /// <summary>
        /// Registers <paramref name="parameter"/> to <paramref name="parameterName" />
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameter">The parameter instance.</param>
        /// <returns>Self (for fluency).</returns>
        IRouteParameterCollection Add(string parameterName, IRouteParameter parameter);

        /// <summary>
        /// Tries to find parameter registered for name <paramref name="parameterName"/>.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameter">The parameter registered to <paramref name="parameterName"/>.</param>
        /// <returns><c>true</c> if parameter was found; <c>false</c> otherwise.</returns>
        bool TryGet(string parameterName, out IRouteParameter parameter);
    }
}
