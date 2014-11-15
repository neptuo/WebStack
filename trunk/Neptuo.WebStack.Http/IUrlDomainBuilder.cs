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
    public interface IUrlDomainBuilder
    {
        /// <summary>
        /// Sets domain name part of URL in the current builder.
        /// </summary>
        /// <param name="domain">Domain name.</param>
        /// <returns>Path builder with domain name set to <paramref name="domain"/>.</returns>
        IUrlPathBuilder Domain(string domain);
    }
}
