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
    public interface IUrlPathBuilder : IUrlQueryStringBuilder
    {
        /// <summary>
        /// Sets path part of URL in the current builder.
        /// </summary>
        /// <param name="path">Path string.</param>
        /// <returns>Query string builder.</returns>
        IUrlQueryStringBuilder Path(string path);
    }
}
