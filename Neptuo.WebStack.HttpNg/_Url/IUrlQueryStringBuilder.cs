using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Part of <see cref="IUrlBuilder"/> for query part of the URL.
    /// </summary>
    public interface IUrlQueryStringBuilder
    {
        /// <summary>
        /// Adds (or overrides) key in query string of the URL.
        /// </summary>
        /// <param name="key">Parameter name.</param>
        /// <param name="value">Parameter value.</param>
        /// <returns>Self (for fluency).</returns>
        IUrlQueryStringBuilder Parameter(string key, string value);

        /// <summary>
        /// Adds (or overrides) key in query string of the URL.
        /// And returns final URL.
        /// </summary>
        /// <param name="key">Parameter name.</param>
        /// <param name="value">Parameter value.</param>
        /// <returns>Built URL.</returns>
        IReadOnlyUrl ParameterToUrl(string key, string value);
    }
}
