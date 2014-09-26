using Neptuo.WebStack.Hosting.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Routing.Segments
{
    internal class RouteSegment
    {
        public IPathSegment Path { get; private set; }

        public List<RouteSegment> Children { get; private set; }
        public IPipelineFactory PipelineFactory { get; set; }

        public RouteSegment(IPathSegment path)
        {
            Guard.NotNull(path, "path");
            Path = path;
        }

        public bool TryMatchUrl(string url, out IPipelineFactory pipelineFactory)
        {
            throw new NotImplementedException();
        }

        public RouteSegment IncludeSegment(IPathSegment newPath)
        {
            StaticPathSegment staticPath = Path as StaticPathSegment;
            StaticPathSegment staticNewPath = newPath as StaticPathSegment;

            if (staticPath != null && staticNewPath != null)
                return IncludeStaticSegment(staticPath, staticNewPath);



            return new RouteSegment(newPath);
        }

        //private RouteSegment IncludeStaticSegment(StaticPathSegment path, StaticPathSegment newPath)
        //{

        //}
    }
}
