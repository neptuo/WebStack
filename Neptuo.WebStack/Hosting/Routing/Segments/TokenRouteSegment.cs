using Neptuo.WebStack.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Routing.Segments
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

        public override RouteSegment Include(RouteSegment newSegment)
        {
            TokenRouteSegment tokenSegment = newSegment as TokenRouteSegment;
            if (tokenSegment != null)
            {
                if (tokenSegment.tokenName == tokenName)
                    return this;
            }

            foreach (RouteSegment routeSegment in Children)
            {
                RouteSegment resultSegment = routeSegment.Include(newSegment);
                if (resultSegment != null)
                    return resultSegment;
            }

            Children.Add(newSegment);
            return newSegment;
        }

        public override string ToString()
        {
            return "{" + tokenName + "}";
        }
    }
}
