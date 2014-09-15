using Neptuo.Tokens;
using Neptuo.WebStack.Hosting.Routing.Segments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Routing
{
    internal class PatternParser
    {
        public PatternParser()
        {
            // Inject dependencies.
        }

        private TokenParser CreateTokenParser()
        {
            TokenParser parser = new TokenParser();
            parser.Configuration.AllowTextContent = true;
            return parser;
        }

        public bool TryBuildUp(string pattern, out List<IRouteSegment> routeSegments)
        {
            //if (!CaseSensitive)
            //    pattern = pattern.ToLowerInvariant();

            int lastIndex = 0;
            List<IRouteSegment> result = new List<IRouteSegment>();
            TokenParser tokenParser = CreateTokenParser();

            tokenParser.OnParsedToken += (sender, e) =>
            {
                if (e.StartPosition > lastIndex)
                    result.Add(new StaticRouteSegment(pattern.Substring(lastIndex, e.StartPosition - lastIndex)));

                result.Add(new ParamRouteSegment(ParameterService.Get(e.Token.Fullname)));
                lastIndex = e.EndPosition + 1;
            };

            if (!tokenParser.Parse(pattern))
            {
                routeSegments = null;
                return false;
            }

            if (pattern.Length > lastIndex)
                routeSegments.Add(new StaticRouteSegment(pattern.Substring(lastIndex)));

            routeSegments = result;
            return true;
        }
    }
}
