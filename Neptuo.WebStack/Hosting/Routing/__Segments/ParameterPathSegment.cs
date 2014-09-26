using Neptuo.WebStack.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Routing.Segments
{
    /// <summary>
    /// Implementation of <see cref="IRouteParameter"/>
    /// </summary>
    public class ParameterPathSegment : IPathSegment
    {
        private readonly string parameterName;
        private readonly IRouteParameter parameter;

        public ParameterPathSegment(string parameterName, IRouteParameter parameter)
        {
            Guard.NotNullOrEmpty(parameterName, "parameterName");
            Guard.NotNull(parameter, "parameter");
            this.parameterName = parameterName;
            this.parameter = parameter;
        }

        public bool TryMatchUrlPart(string url, out string remainingUrl)
        {
            string toMatch = "{" + parameterName + "}";
            if (url.StartsWith(toMatch))
            {
                remainingUrl = url.Substring(toMatch.Length);
                return true;
            }

            remainingUrl = url;
            return false;
        }


        public IPathSegment IncludeSegment(IPathSegment pathSegment)
        {
            ParameterPathSegment newSegment = pathSegment as ParameterPathSegment;
            if (newSegment == null)
                return null;

            if (newSegment == this)
                return this;

            return null;
        }
    }
}
