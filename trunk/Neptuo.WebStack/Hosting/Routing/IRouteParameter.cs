using Neptuo.WebStack.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Routing
{
    /// <summary>
    /// Dynamic part of URL.
    /// </summary>
    public interface IRouteParameter
    {
        /// <summary>
        /// Tries to match part of URL in <see cref="IRouteParameterMatchContext.OriginalUrl"/>.
        /// Remaining part should be placed to <see cref="IRouteParameterMatchContext.RemainigUrl"/> 
        /// and this URL segment then matched by another segment.
        /// </summary>
        /// <param name="context">Context describing URL and current HTTP request.</param>
        /// <returns><c>true</c> is part of URL is matched; <c>false</c> otherwise.</returns>
        bool MatchUrl(IRouteParameterMatchContext context);
    }
}
