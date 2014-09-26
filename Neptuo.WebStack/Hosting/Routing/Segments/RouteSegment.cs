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
        public HashSet<RouteSegment> Children { get; set; }
        public IPipelineFactory PipelineFactory { get; set; }

        public RouteSegment()
        {
            Children = new HashSet<RouteSegment>();
        }

        /// <summary>
        /// Should try to intergrate <paramref name="newSegment"/>. 
        /// If integration is not possible, should return <c>null</c>; otherwise should return corresponding segment.
        /// </summary>
        /// <param name="newSegment">New segment to integrate.</param>
        /// <returns>If integration is not possible, should return <c>null</c>; otherwise should return corresponding segment.</returns>
        public abstract RouteSegment Include(RouteSegment newSegment);
    }
}
