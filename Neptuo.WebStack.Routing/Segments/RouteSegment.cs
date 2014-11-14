using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Routing.Segments
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
        public IRequestHandler RequestHandler { get; set; }

        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        protected RouteSegment()
        {
            Children = new HashSet<RouteSegment>();
        }

        #region Building route tree

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

        #endregion

        #region Resolving url

        /// <summary>
        /// Returns handler register for <paramref name="url"/>, where <paramref name="url"/> starts with this segment.
        /// </summary>
        /// <param name="url">Url to resolve registered handler for.</param>
        /// <returns>Handler for <paramref name="url"/>; <c>null</c> of not found/registered.</returns>
        public abstract IRequestHandler ResolveUrl(string url);

        #endregion
    }
}
