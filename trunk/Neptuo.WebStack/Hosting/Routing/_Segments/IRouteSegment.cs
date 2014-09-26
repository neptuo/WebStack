using Neptuo.WebStack.Hosting.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Routing.Segments
{
    public interface IRouteSegment
    {
        List<IRouteSegment> Children { get; }
        IPipelineFactory PipelineFactory { get; set; }

        bool TryMatchUrl(string url, out IPipelineFactory pipelineFactory);

        void IncludeSegment(IRouteSegment newSegment, IPipelineFactory pipelineFactory);
        IRouteSegment IncludeUrl(string url);
    }
}
