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
        Schema,

        Domain,

        Path,

        VirtualPath
    }
}
