using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Part of <see cref="IUrlBuilder"/> for path of the URL.
    /// </summary>
    public interface IUrlPathBuilder
    {
        /// <summary>
        /// Sets path part of URL in the current builder.
        /// </summary>
        /// <param name="path">Path string.</param>
        /// <returns>Built URL.</returns>
        IReadOnlyUrl Path(string path);
    }
}
