using Neptuo.WebStack.Http;
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
            Ensure.NotNullOrEmpty(tokenName, "tokenName");
            Ensure.NotNull(parameter, "parameter");
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

        public override IRequestHandler ResolveUrl(string url, IHttpContext httpContext)
        {
            IRouteParameterMatchContext matchContext = new RouteParameterMatchContext()
            {
                OriginalUrl = url,
                RemainingUrl = url,
                HttpContext = httpContext
            };

            if (parameter.MatchUrl(matchContext))
            {
                if (String.IsNullOrEmpty(matchContext.RemainingUrl))
                    return RequestHandler;

                foreach (RouteSegment child in Children)
                {
                    IRequestHandler requestHandler = child.ResolveUrl(matchContext.RemainingUrl, httpContext);
                    if (requestHandler != null)
                        return requestHandler;
                }
            }

            return null;
        }

        #endregion

        public override string ToString()
        {
            return "{" + tokenName + "}";
        }
    }

    internal class RouteParameterMatchContext : IRouteParameterMatchContext
    {
        public string OriginalUrl { get; set; }
        public string RemainingUrl { get; set; }
        public IHttpContext HttpContext { get; set; }
    }

}
