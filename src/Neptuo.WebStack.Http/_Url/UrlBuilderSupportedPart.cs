using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// List of supported root parts for <see cref="UrlBuilder"/>.
    /// </summary>
    [Flags]
    public enum UrlBuilderSupportedPart
    {
        /// <summary>
        /// Schema (or protocol) of absolute the URL.
        /// </summary>
        Schema,

        /// <summary>
        /// Domain name.
        /// </summary>
        Host,

        /// <summary>
        /// Path on server.
        /// </summary>
        Path,

        /// <summary>
        /// Path on server relative to the hosting path.
        /// </summary>
        VirtualPath,

        /// <summary>
        /// Query parameters (after '?') in the URL.
        /// </summary>
        QueryString
    }
}
