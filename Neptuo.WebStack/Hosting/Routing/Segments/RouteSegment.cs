using Neptuo.WebStack.Hosting.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Routing.Segments
{
    public abstract class RouteSegment
    {
        public string UrlPart { get; set; }
        public List<RouteSegment> Children { get; protected set; }

        public abstract bool TryMatchUrl(string url, out IPipelineFactory pipelineFactory);

        public abstract void IncludeUrl(string url, IPipelineFactory pipelineFactory);
    }
}
