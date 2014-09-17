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

                IRouteParameter parameter;
                if (parameterCollection.TryGet(e.Token.Fullname, out parameter))
                    result.Add(new ParameterRouteSegment(parameter));

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
