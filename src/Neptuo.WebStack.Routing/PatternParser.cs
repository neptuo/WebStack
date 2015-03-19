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
            Ensure.NotNull(parameterCollection, "parameterCollection");
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
            List<RouteSegment> resultSegments = new List<RouteSegment>();
            TokenParser tokenParser = CreateTokenParser();

            bool result = true;
            tokenParser.OnParsedToken += (sender, e) =>
            {
                if (e.StartPosition > lastIndex)
                    resultSegments.Add(new StaticRouteSegment(pattern.Substring(lastIndex, e.StartPosition - lastIndex)));

                IRouteParameter parameter;
                if (parameterCollection.TryGet(e.Token.Fullname, out parameter))
                    resultSegments.Add(new TokenRouteSegment(e.Token.Fullname, parameter));
                else
                    result = false;

                lastIndex = e.EndPosition;
            };

            if (!tokenParser.Parse(pattern))
            {
                routeSegments = null;
                return false;
            }

            if (pattern.Length > lastIndex)
                resultSegments.Add(new StaticRouteSegment(pattern.Substring(lastIndex)));

            routeSegments = resultSegments;
            return result;
        }
    }
}
