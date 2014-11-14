using Neptuo.WebStack.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Routing.Segments
{
    /// <summary>
    /// Implementation of <see cref="RouteSegment"/> for dynamic parameters.
    /// </summary>
    public class TokenRouteSegment : RouteSegment
    {
        private readonly string tokenName;
        private readonly IRouteParameter parameter;

        public TokenRouteSegment(string tokenName, IRouteParameter parameter)
        {
            Guard.NotNullOrEmpty(tokenName, "tokenName");
            Guard.NotNull(parameter, "parameter");
            this.tokenName = tokenName;
            this.parameter = parameter;
        }

        #region Building route tree

        public override RouteSegment TryInclude(RouteSegment newSegment)
        {
            TokenRouteSegment tokenSegment = newSegment as TokenRouteSegment;
            if (tokenSegment != null)
            {
                if (tokenSegment.tokenName == tokenName)
                    return this;
            }

            return null;
        }

        public override RouteSegment Append(RouteSegment newSegment)
        {
            foreach (RouteSegment routeSegment in Children)
            {
                RouteSegment resultSegment = routeSegment.TryInclude(newSegment);
                if (resultSegment != null)
                    return resultSegment;
            }

            Children.Add(newSegment);
            return newSegment;
        }

        #endregion

        #region Resolving url

        public override IRequestHandler ResolveUrl(string url)
        {
            throw Guard.Exception.NotImplemented();
        }

        #endregion

        public override string ToString()
        {
            return "{" + tokenName + "}";
        }
    }
}
