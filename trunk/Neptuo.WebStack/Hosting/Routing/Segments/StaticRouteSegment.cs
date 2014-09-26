using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Routing.Segments
{
    public class StaticRouteSegment : RouteSegment
    {
        public string UrlPart { get; private set; }

        public StaticRouteSegment(string urlPart)
        {
            Guard.NotNullOrEmpty(urlPart, "urlPart");
            UrlPart = urlPart;
        }

        public override RouteSegment Include(RouteSegment newSegment)
        {
            // If static, run "real inclusion" logic.
            StaticRouteSegment staticSegment = newSegment as StaticRouteSegment;
            if (staticSegment != null)
                return IncludeStaticSegment(staticSegment);

            // Just append.
            foreach (RouteSegment routeSegment in Children)
            {
                RouteSegment resultSegment = routeSegment.Include(newSegment);
                if (resultSegment != null)
                    return resultSegment;
            }

            Children.Add(newSegment);
            return newSegment;
        }

        private RouteSegment IncludeStaticSegment(StaticRouteSegment newSegment)
        {
            bool isOk = false;

            // Find equal chars.
            int index = 0;
            while (IsCharAtEquals(newSegment.UrlPart, UrlPart, index))
                index++;

            // When whole segment url is matched, we are finished.
            if (index == newSegment.UrlPart.Length && index == UrlPart.Length)
                return this;

            // When this segment is matched whole, only append as child.
            if (index == UrlPart.Length)
            {
                newSegment.UrlPart = newSegment.UrlPart.Substring(index);
                isOk = true;
            } 
            else 
            // When at least one char is matched, then divide partialy-matched segment into to two with common parent.
            if (index > 0)
            {
                // Update structure so new segment has shared chars and partialy-matched segment is nested under.
                StaticRouteSegment partSegment = new StaticRouteSegment(UrlPart.Substring(index));
                partSegment.PipelineFactory = PipelineFactory;

                foreach (RouteSegment item in Children)
                    partSegment.Children.Add(item);

                Children.Clear();
                Children.Add(partSegment);

                // Remove shared chars from partialy-matched segment.
                UrlPart = UrlPart.Substring(0, index);
                if (UrlPart != newSegment.UrlPart)
                {
                    newSegment.UrlPart = newSegment.UrlPart.Substring(index);
                    Children.Add(newSegment);
                    return newSegment;
                }
                else
                {
                    return this;
                }
            }

            // Walk through child segments and try to find some matching chars.
            foreach (RouteSegment routeSegment in Children.ToList())
            {
                RouteSegment resultSegment = routeSegment.Include(newSegment);
                if (resultSegment != null)
                    return resultSegment;
            }

            if (isOk)
            {
                Children.Add(newSegment);
                return newSegment;
            }

            return null;
        }

        private string TryMatchUrlPart(string url)
        {
            if (url.StartsWith(UrlPart))
            {
                url = url.Substring(UrlPart.Length);
                return url;
            }
            return url;
        }

        /// <summary>
        /// Returns <c>true</c> when <paramref name="source"/> and <paramref name="target"/> at index <paramref name="index"/> has same chars.
        /// When length of either source or target has length smaller then <paramref name="index"/>, returns <c>false</c>.
        /// </summary>
        /// <param name="source">First value.</param>
        /// <param name="target">Second value.</param>
        /// <param name="index">Index to compare <paramref name="source"/> and <paramref name="target"/> at.</param>
        /// <returns><c>true</c> when <paramref name="source"/> and <paramref name="target"/> are equal at index <paramref name="index"/>.</returns>
        private bool IsCharAtEquals(string source, string target, int index)
        {
            if (source.Length <= index || target.Length <= index)
                return false;

            return source[index] == target[index];
        }

        public override string ToString()
        {
            return UrlPart;
        }
    }
}
