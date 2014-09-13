using Neptuo.WebStack.Hosting.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Routing.Segments
{
    public class StaticRouteSegment : RouteSegment, IStaticRouteSegment
    {
        public IPipelineFactory PipelineFactory { get; private set; }

        public StaticRouteSegment(string urlPart)
        {
            Children = new List<RouteSegment>();
            UrlPart = urlPart;
        }

        public StaticRouteSegment(string urlPart, IPipelineFactory pipelineFactory)
            : this(urlPart)
        {
            PipelineFactory = pipelineFactory;
        }

        /// <summary>
        /// Returns string after matching with <see cref="UrlPart"/>.
        /// Returns <c>null</c> when <paramref name="url"/> doesn't start with <see cref="UrlPart"/>.
        /// </summary>
        /// <param name="url">Url to compare and shrink shared chars.</param>
        /// <returns><paramref name="url"/> without shared first-n chars with <see cref="UrlPart"/>; <c>false</c> when <paramref name="url"/> doesn't start with <see cref="UrlPart"/>.</returns>
        private string TryMatchUrlPart(string url)
        {
            if (url.StartsWith(UrlPart))
            {
                url = url.Substring(UrlPart.Length);
                return url;
            }
            return null;
        }

        public override bool TryMatchUrl(string url, out IPipelineFactory pipelineFactory)
        {
            url = TryMatchUrlPart(url);
            if (url != null)
            {
                if (url.Length != 0)
                {
                    foreach (RouteSegment routeSegment in Children)
                    {
                        if (routeSegment.TryMatchUrl(url, out pipelineFactory))
                            return true;
                    }
                }
                else if (PipelineFactory != null)
                {
                    pipelineFactory = PipelineFactory;
                    return true;
                }
            }

            pipelineFactory = null;
            return false;
        }

        public override void IncludeUrl(string url, IPipelineFactory pipelineFactory)
        {
            // Ignore inlcude request when 'this' segment is not matched.
            url = TryMatchUrlPart(url);
            if (url != null)
            {
                // When no 'new' url to append to this segment, only update pipeline.
                if (url.Length == 0)
                {
                    PipelineFactory = pipelineFactory;
                    return;
                }

                // Only for Static route segments.
                // Walk through child segments and try to find some matching chars.
                foreach (RouteSegment routeSegment in Children.ToList())
                {
                    // Find equal chars.
                    int index = 0;
                    while (IsCharAtEquals(routeSegment.UrlPart, url, index))
                        index++;

                    // When whole segment url is matched, delegate inclusion to segment.
                    if(index == routeSegment.UrlPart.Length)
                    {
                        routeSegment.IncludeUrl(url, pipelineFactory);
                        return;
                    }

                    // When at least one char is matched, then divide partialy-matched segment into to two with common parent.
                    if (index > 0)
                    {
                        // Update structure so new segment has shared chars and partialy-matched segment is nested under.
                        Children.Remove(routeSegment);
                        StaticRouteSegment newRouteSegment = new StaticRouteSegment(routeSegment.UrlPart.Substring(0, index));
                        Children.Add(newRouteSegment);
                        newRouteSegment.Children.Add(routeSegment);

                        // Remove shared chars from partialy-matched segment.
                        routeSegment.UrlPart = routeSegment.UrlPart.Substring(index);

                        // Delegate inclusion to new segment.
                        newRouteSegment.IncludeUrl(url, pipelineFactory);
                        return;
                    }
                }

                // When any of children has same at least one char, include as new child-segment.
                Children.Add(new StaticRouteSegment(url, pipelineFactory));
            }
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
    }
}
