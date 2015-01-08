using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Routing.Segments
{
    public class VirtualPathRouteSegment : StaticRouteSegment
    {
        public VirtualPathRouteSegment()
            : base("~/")
        { }
    }
}
