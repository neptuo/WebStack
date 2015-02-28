using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Builder for <see cref="IReadOnlyUrl"/>.
    /// </summary>
    public interface IUrlBuilder : IUrlHostBuilder, IUrlPathBuilder
    {
        /// <summary>
        /// Parses <paramref name="url"/> info <see cref="IReadOnlyUrl"/>.
        /// </summary>
        /// <param name="url">String representation of URL.</param>
        /// <returns></returns>
        IReadOnlyUrl FromUrl(string url);

        /// <summary>
        /// Sets schema part of URL in the current builder.
        /// </summary>
        /// <param name="schema">Schema name.</param>
        /// <returns>Domain name builder with schema set to <paramref name="schema"/>.</returns>
        IUrlHostBuilder Schema(string schema);

        /// <summary>
        /// Creates virtual URL (starts with '~/').
        /// </summary>
        /// <param name="virtualPath">Virtual path.</param>
        /// <returns>Built URL.</returns>
        IReadOnlyUrl VirtualPath(string virtualPath);
    }
}
