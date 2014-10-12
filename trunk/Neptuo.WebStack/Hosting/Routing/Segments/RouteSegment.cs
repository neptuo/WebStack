using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Routing.Segments
{
    /// <summary>
    /// Base for segment route.
    /// Represents single part of route, allows to integrace other segments. 
    /// Also can contain route handler for handling request to route represented by this segment.
    /// </summary>
    public abstract class RouteSegment
    {
        /// <summary>
        /// Collection of child segments.
        /// </summary>
        protected HashSet<RouteSegment> Children { get; private set; }

        /// <summary>
        /// If set, contains route handler for this segment.
        /// </summary>
        public IRouteHandler RouteHandler { get; set; }

        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        protected RouteSegment()
        {
            Children = new HashSet<RouteSegment>();
        }

        /// <summary>
        /// Must first check for matching current segment.
        /// </summary>
        /// <param name="newSegment">New segment to integrate.</param>
        /// <returns>If integration is not possible, should return <c>null</c>; otherwise should return corresponding segment.</returns>
        public abstract RouteSegment TryInclude(RouteSegment newSegment);

        /// <summary>
        /// Should just append <paramref name="newSegmnet"/> to children segments.
        /// </summary>
        /// <param name="newSegment">New segment to integrate.</param>
        /// <returns>Newly integrated segment corresponding to <paramref name="newSegment"/>.</returns>
        public abstract RouteSegment Append(RouteSegment newSegment);

        /// <summary>
        /// Returns enumeration of all child segments.
        /// </summary>
        /// <returns>Enumeration of all child segments.</returns>
        public IEnumerable<RouteSegment> EnumerateChildren()
        {
            return Children;
        }
    }
}
