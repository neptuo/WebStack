using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Routing.Segments
{
    public interface IStaticRouteSegment
    {
        string UrlPart { get; set; }
        List<RouteSegment> Children { get; protected set; }
    }
}
