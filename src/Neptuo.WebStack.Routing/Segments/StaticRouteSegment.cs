using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Routing.Segments
{
    /// <summary>
    /// Represents segment of static route part which can be splited if needed.
    /// </summary>
    public class StaticRouteSegment : RouteSegment
    {
        /// <summary>
        /// Static part of route.
        /// </summary>
        public string UrlPart { get; private set; }

        /// <summary>
        /// Creates new instance with value of part in <paramref name="urlPath"/>.
        /// </summary>
        /// <param name="urlPart">Static part of route.</param>
        public StaticRouteSegment(string urlPart)
        {
            Ensure.NotNullOrEmpty(urlPart, "urlPart");
            UrlPart = urlPart;
        }

        #region Building route tree

        public override RouteSegment TryInclude(RouteSegment newSegment)
        {
            // If static, run "real inclusion" logic.
            StaticRouteSegment staticSegment = newSegment as StaticRouteSegment;
            if (staticSegment != null)
                return IncludeStaticSegment(staticSegment);

            // Other types are not supported for inclusion.
            return null;
        }

        public override RouteSegment Append(RouteSegment newSegment)
        {
            // Try to include in child segments.
            foreach (RouteSegment routeSegment in Children)
            {
                RouteSegment resultSegment = routeSegment.TryInclude(newSegment);
                if (resultSegment != null)
                    return resultSegment;
            }

            // If inclusion is not possible, just append as new child.
            Children.Add(newSegment);
            return newSegment;
        }

        private RouteSegment IncludeStaticSegment(StaticRouteSegment newSegment)
        {
            // Find first-n equal chars.
            int index = 0;
            while (IsCharAtEquals(newSegment.UrlPart, UrlPart, index))
                index++;

            // When segment urls are the same, return self.
            if (index == newSegment.UrlPart.Length && index == UrlPart.Length)
                return this;

            // When this segment is matched whole, only append as child.
            if (index == UrlPart.Length)
            {
                // Update new url by removing shared characters.
                newSegment.UrlPart = newSegment.UrlPart.Substring(index);

                // I'm matched, so just append to children (with try include on existing).
                return Append(newSegment);
            }

            // When at least one char is matched, then divide partialy-matched segment into to two with common parent.
            if (index > 0)
            {
                // Update structure so new segment has shared chars and partialy-matched segment is nested under.
                StaticRouteSegment partSegment = new StaticRouteSegment(UrlPart.Substring(index));
                partSegment.Target = Target;

                // All my children are copied to the newly created segment.
                foreach (RouteSegment item in Children)
                    partSegment.Children.Add(item);

                // Clean up and append the newly created segment.
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

            // Inclusion is not possible.
            return null;
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

        #endregion

        #region Resolving url

        public override object ResolveUrl(string url, IHttpContext httpContext)
        {
            Ensure.NotNull(url, "url");
            if (url.StartsWith(UrlPart))
            {
                string remainingUrl = url.Substring(UrlPart.Length);
                if (String.IsNullOrEmpty(remainingUrl))
                    return Target;

                foreach (RouteSegment child in Children)
                {
                    object target = child.ResolveUrl(remainingUrl, httpContext);
                    if (target != null)
                        return target;
                }
            }

            return null;
        }

        #endregion

        public override string ToString()
        {
            return UrlPart;
        }
    }
}
