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
        string UrlPart { get; set; }
        List<IRouteSegment> Children { get; }

        bool TryMatchUrl(string url, out IPipelineFactory pipelineFactory);

        void IncludeUrl(string url, IPipelineFactory pipelineFactory);
    }
}
