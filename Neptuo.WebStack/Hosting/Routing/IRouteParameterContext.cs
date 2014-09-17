using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Routing
{
    /// <summary>
    /// Context object for matching route parameter.
    /// </summary>
    public interface IRouteParameterMatchContext
    {
        /// <summary>
        /// Original URL to match by parameter.
        /// </summary>
        string OriginalUrl { get; }

        /// <summary>
        /// After parameter matches URL, this property should contain remain part of URL that should by matched by another segment.
        /// </summary>
        string RemainingUrl { get; set; }

        /// <summary>
        /// Current http context.
        /// </summary>
        IHttpContext HttpContext { get; }
    }
}
