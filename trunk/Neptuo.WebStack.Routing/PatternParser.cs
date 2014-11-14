using Neptuo.Tokens;
using Neptuo.WebStack.Routing.Segments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Routing
{
    internal class PatternParser
    {
        private readonly IRouteParameterCollection parameterCollection;

        public PatternParser(IRouteParameterCollection parameterCollection)
        {
            Guard.NotNull(parameterCollection, "parameterCollection");
            this.parameterCollection = parameterCollection;
        }

        private TokenParser CreateTokenParser()
        {
            TokenParser parser = new TokenParser();
            parser.Configuration.AllowTextContent = true;
            parser.Configuration.AllowMultipleTokens = true;
            return parser;
        }

        public bool TryBuildUp(string pattern, out List<RouteSegment> routeSegments)
        {
            //if (!CaseSensitive)
            //    pattern = pattern.ToLowerInvariant();

            int lastIndex = 0;
            List<RouteSegment> result = new List<RouteSegment>();
            TokenParser tokenParser = CreateTokenParser();

            tokenParser.OnParsedToken += (sender, e) =>
            {
                if (e.StartPosition > lastIndex)
                    result.Add(new StaticRouteSegment(pattern.Substring(lastIndex, e.StartPosition - lastIndex)));

                IRouteParameter parameter;
                if (parameterCollection.TryGet(e.Token.Fullname, out parameter))
                    result.Add(new TokenRouteSegment(e.Token.Fullname, parameter));

                lastIndex = e.EndPosition + 1;
            };

            if (!tokenParser.Parse(pattern))
            {
                routeSegments = null;
                return false;
            }

            if (pattern.Length > lastIndex)
                result.Add(new StaticRouteSegment(pattern.Substring(lastIndex)));

            routeSegments = result;
            return true;
        }
    }
}
