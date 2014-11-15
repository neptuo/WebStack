using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Part of <see cref="IUrlBuilder"/> for building domain name of the URL.
    /// </summary>
    public interface IUrlHostBuilder
    {
        /// <summary>
        /// Sets domain name part of URL in the current builder.
        /// </summary>
        /// <param name="host">Domain name + post.</param>
        /// <returns>Path builder with domain name + port set to <paramref name="host"/>.</returns>
        IUrlPathBuilder Host(string host);
    }
}
