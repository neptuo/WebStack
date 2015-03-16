using Neptuo.Collections.Specialized;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Routing.Segments
{
    /// <summary>
    /// "Empty" segment that everthing delegates to children.
    /// </summary>
    public class SchemaSegment : RouteSegment
    {
        public override RouteSegment TryInclude(RouteSegment newSegment)
        {
            foreach (RouteSegment child in Children)
            {
                RouteSegment newChildSegment = child.TryInclude(newSegment);
                if (newChildSegment != null)
                    return newChildSegment;
            }

            return null;
        }

        public override RouteSegment Append(RouteSegment newSegment)
        {
            Children.Add(newSegment);
            return newSegment;
        }

        public override object ResolveUrl(string url, IKeyValueCollection routeValues)
        {
            foreach (RouteSegment child in Children)
            {
                object target = child.ResolveUrl(url, routeValues);
                if (target != null)
                    return target;
            }

            return null;
        }
    }
}
