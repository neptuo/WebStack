using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Routing.Segments
{
    public class PathRouteSegment : StaticRouteSegment
    {
        public PathRouteSegment()
            : base("~/")
        { }
    }
}
