using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Routing.Segments
{
    public interface IPathSegment
    {
        bool TryMatchUrlPart(string url, out string remainingUrl);

        IPathSegment IncludeSegment(IPathSegment pathSegment);
    }
}
