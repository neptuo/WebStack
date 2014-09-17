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
    public class ParameterRouteSegment : IRouteSegment
    {
        private readonly IRouteParameter parameter;

        public ParameterRouteSegment(IRouteParameter parameter)
        {
            Guard.NotNull(parameter, "parameter");
            this.parameter = parameter;
        }
    }
}
